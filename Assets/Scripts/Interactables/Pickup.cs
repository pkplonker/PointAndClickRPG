namespace Interactables
{
	public class Pickup : Interactable
	{
		public override void Interact()
		{
			base.Interact();
			Destroy(gameObject);
		}
	}
}
