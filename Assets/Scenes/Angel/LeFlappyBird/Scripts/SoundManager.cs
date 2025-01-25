using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Tamaño de la ventana de muestra utilizada para el cálculo del volumen.
    public int sampleWindow = 64;

    // Clip de audio donde se almacenará la entrada del micrófono.
    private AudioClip microphoneClip;

    void Start()
    {
        // Inicia la grabación del micrófono al iniciar el juego.
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip()
    {
        // Obtiene el nombre del primer micrófono disponible.
        string MicrophoneName = Microphone.devices[0];

        // Inicia la grabación del micrófono en un clip de audio con una duración de 20 segundos.
        microphoneClip = Microphone.Start(MicrophoneName, true, 20, AudioSettings.outputSampleRate);
    }

    // Obtiene la intensidad del sonido en la posición actual del micrófono.
    public float GetLoudnessFromMicrophone()
    {
        // Obtiene la posición actual de la grabación del micrófono y analiza el volumen.
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    // Calcula la intensidad del sonido a partir del clip de audio capturado.
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        // Determina la posición de inicio de la ventana de muestras.
        int startPosition = clipPosition - sampleWindow;

        // Evita errores si la posición de inicio es negativa.
        if (startPosition < 0)
        {
            return 0;
        }

        // Almacena los datos de la onda de sonido en un arreglo de tamaño `sampleWindow`.
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // Calcula la suma total de los valores absolutos de la onda para estimar la intensidad.
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        // Retorna la intensidad promedio del sonido.
        return totalLoudness / sampleWindow;
    }
}
