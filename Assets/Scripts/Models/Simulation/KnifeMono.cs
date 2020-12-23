using UnityEngine;

namespace Models.Simulation
{
    public class KnifeMono : MonoBehaviour
    {
        private const string BrickTag = "brick";
    
        public Vector3 MovementVector = Vector3.zero;
    
        void Start()
        {
        
        }

        public void UpdateObject(float physicsDeltaTime)
        {
            transform.localPosition += transform.localRotation * (MovementVector * physicsDeltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(BrickTag))
            {
                var rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = false;
            }
        }
    }
}
