using UnityEngine;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public List<Transform> waypoints;   // Lista de pontos da rota
    public float speed = 6f;            // Velocidade do carro
    public float stopDistance = 1.5f;   // Distância mínima para parar atrás de outro carro
    private int currentWaypointIndex = 0;

    // Referência opcional a semáforo
    public TrafficLight trafficLight;

    void Update()
    {
        if (waypoints.Count == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;

        // Verifica semáforo
        if (trafficLight != null && trafficLight.currentState == TrafficLight.LightState.Red)
        {
            // Se estiver perto do semáforo, para
            if (Vector3.Distance(transform.position, target.position) < 5f)
                return;
        }

        // Verifica colisão simples com outro carro à frente
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, stopDistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                return; // para se tiver outro carro na frente
            }
        }

        // Movimento em direção ao próximo waypoint
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(direction);

        // Se chegou ao waypoint, passa para o próximo
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}
