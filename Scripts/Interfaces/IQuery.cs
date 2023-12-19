/*
 USTRAP INTERFACE
 IQUERY
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

namespace UStrap
{
    /// <summary>
    /// Defines ui elements that can be queried with a tag.
    /// </summary>
    public interface IQuery
    {
        string queryTag { get; }
    }
}