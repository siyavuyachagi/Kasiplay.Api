using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum StorageProvider
    {
        Cloudinary,
        GoogleDrive,
        OneDrive,
        Other
    }

    public enum FileType
    {
        Image,
        Document,
        Video,
        Audio,
        Archive,
        Other
    }

    public class FileResource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public long FileSize { get; set; } // in bytes
        public FileType Type { get; set; } = FileType.Other;

        // Source info
        public StorageProvider StorageProvider { get; set; } = StorageProvider.Other;
        public string? FolderId { get; set; }
        public string? FileId { get; set; }

        public string? UploadedById { get; set; }
        [ForeignKey(nameof(UploadedById))]
        public virtual ApplicationUser? UploadedBy { get; set; }

        public Guid ForeignKey { get; set; } // The entity this file belongs to (polymorphic link)

        // URL references
        public string? PhysicalUrl { get; set; }
        public string? RelativeUrl { get; set; } // e.g. /uploads/players/1234.jpg
        public string? ViewUrl { get; set; } // direct file access (e.g., CDN link)
        public string? DownloadUrl { get; set; } // pre-signed download link

        // Access control
        public bool IsPublic { get; set; } = false; // whether anyone can view/download

        // System metadata
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
