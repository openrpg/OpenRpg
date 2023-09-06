namespace OpenRpg.Core.Common
{
    /// <summary>
    /// This interface implies the object does not need runtime uniqueness and is something that can be looked up from
    /// a pre populated data store via its id such as a RaceTemplate or an ItemTemplate.
    /// </summary>
    /// <remarks>
    /// The id is an int as it is very unlikely a game would have int.MaxValue ItemTemplates, RaceTemplates etc
    ///
    /// For those entities which DO need to be globally unique there is an IIsUnique interface which provides a GUID
    /// </remarks>
    public interface IHasDataId
    {
        /// <summary>
        /// The unique id of this data item
        /// </summary>
        int Id { get; }
    }
}