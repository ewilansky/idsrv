using System;
namespace client
{
    public class FileModel
    {
        public FileStatuses FileStatuses { get; set; }
    }

    public class FileStatuses
    {
        public Filestatus[] FileStatus { get; set; }
    }

    public class Filestatus
    {
        public long AccessTime { get; set; }
        public int BlockSize { get; set; }
        public int ChildrenNum { get; set; }
        public int FileId { get; set; }
        public string Group { get; set; }
        public int Length { get; set; }
        public long ModificationTime { get; set; }
        public string Owner { get; set; }
        public string PathSuffix { get; set; }
        public string Permission { get; set; }
        public int Replication { get; set; }
        public int StoragePolicy { get; set; }
        public string Type { get; set; }
    }
}