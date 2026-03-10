using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BusController : MonoBehaviour
{
    public List<Transform> waypoints;     // Lista de pontos da rota
    public List<int> busStops;            // Índices dos waypoints que são paradas
    public float speed = 4f;              // Velocidade do ônibus
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    // Referência opcional a semáforo
    public TrafficLight trafficLight;

    void Update()
    {
        if (waypoints.Count == 0 || isWaiting) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;

        // Verifica semáforo
        if (trafficLight != null && trafficLight.currentState == TrafficLight.LightState.Red)
        {
            if (Vector3.Distance(transform.position, target.position) < 5f)
                return; // para no vermelho
        }

        // Movimento em direção ao próximo waypoint
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(direction);

        // Se chegou ao waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            // Se for uma parada de ônibus, espera alguns segundos
            if (busStops.Contains(currentWaypointIndex))
            {
                StartCoroutine(WaitAtStop(3f)); // espera 3 segundos
            }

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }

    IEnumerator WaitAtStop(float waitTime)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
