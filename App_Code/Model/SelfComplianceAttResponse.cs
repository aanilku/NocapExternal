using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SelfComplianceAttResponse
/// </summary>
public class SelfComplianceAttResponse
{
    public class AllImage
    {
        public string img_id { get; set; }
        public string selfcomp_id { get; set; }
        public string imgUsedFor { get; set; }
        public int imgType { get; set; }
        public string imgTitle { get; set; }
        public string uploadPath { get; set; }
        public string createAt { get; set; }
        public string imgStatus { get; set; }
        public long appCode { get; set; }
        public string appNum { get; set; }
        public string nocNum { get; set; }
        public byte[] file_type { get; set; }
        public object attachmentName { get; set; }
        public object contentType { get; set; }
        public object fileExt { get; set; }
    }

    public class Root
    {
        public List<AllImage> all_image { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string file_url { get; set; }
    }
}