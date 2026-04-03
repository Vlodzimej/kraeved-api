using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<IEnumerable<Comment>> GetCommentsByGeoObjectId(int geoObjectId);
        Task<Comment?> GetLatestCommentByGeoObjectId(int geoObjectId);
        Task<Comment> AddComment(int geoObjectId, int userId, string text);
        Task<Comment?> DeleteComment(int commentId, int userId);
    }
}
