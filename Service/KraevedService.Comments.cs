using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<IEnumerable<Comment>> GetCommentsByGeoObjectId(int geoObjectId)
        {
            return _unitOfWork.CommentsRepository.Get(
                x => x.GeoObjectId == geoObjectId,
                includeProperties: "User",
                orderBy: q => q.OrderByDescending(c => c.CreatedAt)
            ).ToList();
        }

        public async Task<Comment> AddComment(int geoObjectId, int userId, string text)
        {
            var comment = new Comment
            {
                GeoObjectId = geoObjectId,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.UtcNow,
            };
            _unitOfWork.CommentsRepository.Insert(comment);
            await _unitOfWork.SaveAsync();
            return comment;
        }

        public async Task<Comment?> DeleteComment(int commentId, int userId)
        {
            var comment = _unitOfWork.CommentsRepository.GetByID(commentId);
            if (comment == null || comment.UserId != userId) return null;

            _unitOfWork.CommentsRepository.Delete(comment);
            await _unitOfWork.SaveAsync();
            return comment;
        }
    }
}
