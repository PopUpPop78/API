namespace Data.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int? id, string name) : base($"Id {id} couldn't be found in entity {name}")
        {

        }
    }
}