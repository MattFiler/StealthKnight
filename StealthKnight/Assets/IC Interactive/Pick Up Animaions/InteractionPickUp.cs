/*
using UnityEngine;
using System.Collections;

public class InteractionPickUp : InteractiveObject {

	public enum PickUpType {Firearm, Magazine, Item}
	public PickUpType puType;

    public string magazineFirearmName;
	public Item item;
    public GameObject firearm;

	public Rigidbody physics;
	public GameObject physicsColliders;
	public Transform interactionColliders;
	private Transform horizontalLookAt;

	private float grabHeight;

	public override bool InteractionConditionsCheck (Character character) {
		if (puType == PickUpType.Magazine) {
			return character.weaponsController.MagazinePickUpCheck();
		}
		if (puType == PickUpType.Item) {
			return (item.volume + character.inventory.occupiedVolume < character.inventory.maxVolume);
		}
		else return true;
	}

	void Awake () {
		horizontalLookAt = new GameObject ("HLA").transform;
		horizontalLookAt.parent = GUIPosition;
	}

	void Update () {
		if (isAvailable) interactionColliders.position = GUIPosition.position;
	}

	public override void MoveColliders (Character character) {
		base.MoveColliders (character);
		horizontalLookAt.transform.position = character.transform.position;
		horizontalLookAt.LookAt (GUIPosition);
		Vector3 horizontalForward = horizontalLookAt.transform.forward;
		horizontalForward.y = 0f;
		interactionColliders.forward = horizontalForward;
		grabHeight = GUIPosition.position.y - character.transform.position.y;
		Vector3 newHelpersPosition = GUIPosition.position;
		newHelpersPosition.y = character.transform.position.y;
		interactionColliders.position = newHelpersPosition;
	}

	public override void ReachInteraction (Character character) {
		base.ReachInteraction (character);
		character.animator.SetFloat (Animator.StringToHash ("GrabHeight"), grabHeight);
		character.animator.SetTrigger (Animator.StringToHash ("GrabNothing"));
	}

	public override void StartInteraction (Character character) {
		base.StartInteraction (character);

		physicsColliders.SetActive (false);
		physics.isKinematic = true;

		if (puType == PickUpType.Firearm) {
            character.weaponsController.PickUpFirearm(firearm);
		}
		else{
			physics.gameObject.SetActive (false);
		}
	}

	public override void InterruptInteraction (Character character) {
		base.InterruptInteraction (character);
	}

	public override void DropItem (Character character) {
		base.DropItem (character);
		interactionColliders.gameObject.SetActive (true);
		MoveColliders (character);
		physicsColliders.SetActive (true);
		physics.isKinematic = false;
		physics.velocity = character.ragdoll.velocity;
	}

	public override void RetrieveControlToCharacter (Character character) {
		interactionColliders.gameObject.SetActive (false);

		if (puType == PickUpType.Magazine) {
            character.weaponsController.AddMagazine(magazineFirearmName);
		}

		if (puType == PickUpType.Item) {
			character.inventory.AddItem (item);
		}
	}
}
*/