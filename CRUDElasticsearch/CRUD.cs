using Nest;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRUDElasticsearch
{
    internal class CRUD
    {
        #region Get document info based on the ID

        public static Tuple<string, string, string, string> getDocument(string searchID)
        {
            string id = "";
            string name = "";
            string originalVoiceActor = "";
            string animatedDebut = "";

            var response = ConnectionToES.EsClient().Search<DocumentAttributes>(s => s
                .Index("disney")
                .Type("character")
                .Query(q => q.Term(t => t.Field("_id").Value(searchID)))); //Search based in _id                

            //Assigining value to their controller
            foreach (var hit in response.Hits)
            {
                id = hit.Id.ToString();// Source.id.ToString();
                name = hit.Source.name.ToString();
                originalVoiceActor = hit.Source.original_voice_actor.ToString();
                animatedDebut = hit.Source.animated_debut.ToString();
            }

            return Tuple.Create(id, name, originalVoiceActor, animatedDebut);
        }

        #endregion Get document info based on the ID

        #region Insert document with on ID
        /*
        public static bool insertDocument(string searchID, string tbxname, string tbxOriginalVoiceActor, string tbxAnimatedDebut)
        {
            bool status;

            var myJson = new
            {
                name = tbxname,
                original_voice_actor = tbxOriginalVoiceActor,
                animated_debut = tbxAnimatedDebut
            };

            var response = ConnectionToES.EsClient().Index(myJson, i => i
                .Index("tdindex")

                .Type("phonetic")
                .Id(searchID)
                .Refresh());

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }
        */
        /*
        public static bool insertDocument(string searchID, object obj)
        {
            bool status;

            var response = ConnectionToES.EsClient().Index(obj, i => i
                .Index("tdindex")
                .Type("phonetic")
                .Id(searchID)
                .Refresh());

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }


        */
        public static bool insertDocument(object obj, int searchID)
        {
            var client = ConnectionToES.EsClient();

            bool status;
            /*
            var createIndexResponse = client.CreateIndex("testing", c => c
     .Mappings(ms => ms
         .Map<DocumentModel>(m => m
             .Properties(ps => ps

             ).AutoMap()
         )

     )
   );*/

            
            var response = client.Index(obj, i => i
                .Index("antelope")
                .Type("documentModel")
                .Id(searchID)
                .Refresh());
                
            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        #endregion Insert document with on ID

        public static DataTable getAllDocument()
        {
            DataTable dataTable = new DataTable("character");
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Original Voice Actor", typeof(string));
            dataTable.Columns.Add("Animated Debut", typeof(string));

            var response = ConnectionToES.EsClient().Search<DocumentAttributes>(s => s
                .Index("disney")
                .Type("character")
                .From(0)
                .Size(1000)
                .Query(q => q.MatchAll()));

            foreach (var hit in response.Hits)
            {
                dataTable.Rows.Add(hit.Id.ToString(), hit.Source.name.ToString(), hit.Source.original_voice_actor.ToString(), hit.Source.animated_debut.ToString());
            }

            return dataTable;
        }



        ///Updating a Document can be done in trhee way
        ///1. Update by Partial Document
        ///2. Update by Index Query
        ///3. Update by Script
        ///Here we demonstrated Update by Partial Document and  Update by Index Query. User can choose any of these from below.
        ///Just comment one part and uncomment another.

        public static bool updateDocument(string searchID, string tbxname, string tbxOriginalVoiceActor, string tbxAnimatedDebut)
        {
            bool status;

            //Update by Partial Document
            //var response = ConnectionToES.EsClient().Update<DocumentAttributes, UpdateDocumentAttributes>(searchID, d => d
            //    .Index("disney")
            //    .Type("character")
            //    .Doc(new UpdateDocumentAttributes
            //    {
            //        name = tbxname,
            //        original_voice_actor = tbxOriginalVoiceActor,
            //        animated_debut = tbxAnimatedDebut
            //    }));


            //End of Update by Partial Document

            //Update by Index Query
            var myJson = new
            {
                name = tbxname,
                original_voice_actor = tbxOriginalVoiceActor,
                animated_debut = tbxAnimatedDebut
            };

            var response = ConnectionToES.EsClient().Index(myJson, i => i
                .Index("disney")
                .Type("character")
                .Id(searchID)
                .Refresh());
            //End of Update by Index Query

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }



        public static bool deleteDocument(string searchID)
        {
            bool status;

            var response = ConnectionToES.EsClient().Delete<DocumentAttributes>(searchID, d => d
                .Index("disney")
                .Type("character"));

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

    }

    public class DocumentModel
    {
        public string Filename { get; set; }
        public int FolderID { get; set; }
        public string uploadUser { get; set; }
        public string UploadDate { get; set; }
        public string DocumentType { get; set; }
        public bool AccessControl { get; set; }
        [Nested]
        public List<CommentList> Comments { get; set; }
        [Nested]
        public List<Metadata> Metadata { get; set; }

        public string CopyToAll { get; set; }


    }

    public class CommentList
    {
        public string CreateDate { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
    }

    public class Metadata
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class FuzzySearchModel
    {

        // admin will define mapped values as below from config/admin api
        public string upload_user { get; set; }
        public string upload_date_time { get; set; } //maybe not requires for soundex
        public string documentName { get; set; }
        public List<MetadataKeys> MetadataKeys { get; set; } //for comments -- look into searching comments meta datas >.. Ocr data after

    }
    public class MetadataKeys
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}