using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class ShieldManager : MonoBehaviourPun
    {
        // capsule material to change when player takes the shield
        public Material material;

        public float ShieldTimeEffect = 10f;

        //when player try to take the shield
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // turn on shield
                other.GetComponent<PlayerManager>().ToggleShield();
                
                // make capsule disappear
                GetComponent<MeshRenderer>().enabled = false;
                // prevent duplicate pick ups
                GetComponent<CapsuleCollider>().enabled = false;

                //switch capsule material, save the previous material
                var temp = other.gameObject.GetComponent<MeshRenderer>().material;
                other.gameObject.GetComponent<MeshRenderer>().material = material;
                material = temp;
                // call the function that disable the shield
                StartCoroutine(TurnOffShield(other.gameObject.GetComponent<MeshRenderer>()));
            }
        }

        // this function disable the shield, after ShieldTimeEffect seconds
        // those are the opposite steps from enable the shield
        // also destroy the shield`s game object
        private IEnumerator TurnOffShield(MeshRenderer other)
        {
            yield return new WaitForSeconds(ShieldTimeEffect);
            other.GetComponent<PlayerManager>().ToggleShield();
            var temp = other.material;
            other.material = material;
            material = temp;
            Destroy(this.gameObject);
        }
    }
}
