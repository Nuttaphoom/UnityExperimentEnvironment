using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("Properties for adjusting the interaction")]
    [SerializeField]
    private float _radius ;

    [Header("Event Channels : Boardcast to")]
    [SerializeField]
    private ItemEventChannelSO _pickUpEventChannel ;


    private Interaction _currentInteraction = null ;


    public Interaction  GetPotentialInteraction()
    {
         float closestDist = Mathf.Infinity; 

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up);

        for (int i = 0; i < hits.Length; i++)
        {
            if (closestDist < hits[i].distance)
                continue; 

            if (hits[i].collider.GetComponent<CollectableItem>()) {
                _currentInteraction = new Interaction(InteractionType.Pickable, hits[i].collider.gameObject); 
            }else if (hits[i].collider.GetComponent<NPCStepControl>())
            {
                _currentInteraction = new Interaction(InteractionType.NPC, hits[i].collider.gameObject);
            }
        }
 
        return (_currentInteraction) ;
    }

    public void Collect( GameObject retriever)
    {
        GameObject itemObject = _currentInteraction.interactableObject; 

        if (_pickUpEventChannel != null)
        {
            ItemSO currentItem = itemObject.GetComponent<CollectableItem>().GetItemSO();
            _pickUpEventChannel.RaiseEvent( retriever, currentItem ) ;
        }

        Destroy(itemObject); //TODO: maybe move this destruction in a more general manger, to implement a removal SFX
    }

    public void InteractWithNPC(NPCStepControl retriever)
    {
        retriever.InteractionWithProtagonist();
    }

}
public enum InteractionType
{
    None    ,
    Pickable,
    NPC, 
}
public class Interaction
{
    public InteractionType interactionType;
    public GameObject interactableObject;

    public Interaction(InteractionType t, GameObject obj)
    {

        interactionType = t;
        interactableObject = obj;
    }
}
