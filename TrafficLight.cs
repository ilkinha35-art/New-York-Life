using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState { Green, Yellow, Red }
    public LightState currentState = LightState.Green;

    public float greenTime = 5f;
    public float yellowTime = 2f;
    public float redTime = 5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case LightState.Green:
                if (timer > greenTime) ChangeState(LightState.Yellow);
                break;

            case LightState.Yellow:
                if (timer > yellowTime) ChangeState(LightState.Red);
                break;

            case LightState.Red:
                if (timer > redTime) ChangeState(LightState.Green);
                break;
        }
    }

    void ChangeState(LightState newState)
    {
        currentState = newState;
        timer = 0f;

        // Aqui você pode trocar materiais/cores da luz
        // Exemplo: mudar cor de um objeto "LightRenderer"
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            switch (currentState)
            {
                case LightState.Green:
                    rend.material.color = Color.green;
                    break;
                case LightState.Yellow:
                    rend.material.color = Color.yellow;
                    break;
                case LightState.Red:
                    rend.material.color = Color.red;
                    break;
            }
        }

        Debug.Log("Semáforo mudou para: " + currentState);
    }
}
