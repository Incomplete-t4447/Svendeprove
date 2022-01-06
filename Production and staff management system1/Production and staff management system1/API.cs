using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SAHB.GraphQL;
using SAHB.GraphQLClient;
using SAHB.GraphQLClient.Extentions;
using SAHB.GraphQLClient.FieldBuilder.Attributes;
using SAHB.GraphQLClient.QueryGenerator;

namespace Production_and_staff_management_system1
{
    class Api
    {
        const string APIIP = "http://pihl.un1c0rn.dk:8080";
        IGraphQLHttpClient client = GraphQLHttpClient.Default();

        public async Task<JObject> checkIndUd(string kortnummer)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("checkIndUd",
                    checkIndUd =>
                        checkIndUd
                            .Argument("kortnummer", "String", "kortnummer", true)
                            .Field("status")
                            .Field("error")
                            ),
                            APIIP, arguments: new[] { new GraphQLQueryArgument("kortnummer", kortnummer)});
            var response = await query.Execute();
            JObject result = response["checkIndUd"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }

        public async Task<JArray> getCheckIndUd()
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("getCheckIndUd",
                        getCheckIndUd =>
                            getCheckIndUd
                                .Field("medarbejderNavn")
                                .Field("medarbejderKort")
                                .Field("checkInd")
                                .Field("checkUd")
        
                    ),
                APIIP 
        );

        var response = await query.Execute();
        JArray result = response["getCheckIndUd"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }
        public async Task<JArray> getMedarbejder()
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("medarbejdere",
                        medarbejdere =>
                            medarbejdere
                                .Field("medarbejderNavn")
                                .Field("medarbejderKort")
                                .Field("type")
                                .Field("telefonNummer")

                    ),
                APIIP
            );

            var response = await query.Execute();
            JArray result = response["medarbejdere"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }
        public async Task<JObject> tilføjMedarbejder(string name, string type, string kortNummer , int tlfNummer)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("opretMedarbejder",
                        opretMedarbejder =>
                            opretMedarbejder
                                .Argument("navn", "string", "navn", true)
                                .Argument("type", "string", "type", true)
                                .Argument("kortnummer", "string", "kortnummer", true)
                                .Argument("tlfnummer", "int", "tlfnummer", true)
                                .Field("status")
                                .Field("error")
                             

                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("navn", name), new GraphQLQueryArgument("type", type), new GraphQLQueryArgument("kortnummer", kortNummer), new GraphQLQueryArgument("tlfnummer", tlfNummer)});

            var response = await query.Execute();
            JObject result = response["opretMedarbejder"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }
        public async Task<JObject> redigerMedarbejder(string name, string type, string kortNummer, int tlfNummer, string nytKortnummer = null)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("redigerMedarbejder",
                        redigerMedarbejder =>
                            redigerMedarbejder
                                .Argument("navn", "string", "navn", true)
                                .Argument("type", "string", "type", true)
                                .Argument("kortnummer", "string", "kortnummer", true)
                                .Argument("tlfnummer", "int", "tlfnummer", true)
                                .Argument("nytKortnummer", "string", "nytKortnummer", true)
                                .Field("status")
                                .Field("error")


                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("navn", name), 
                    new GraphQLQueryArgument("type", type), 
                    new GraphQLQueryArgument("kortnummer", kortNummer), 
                    new GraphQLQueryArgument("tlfnummer", tlfNummer), 
                    new GraphQLQueryArgument("nytKortnummer", nytKortnummer) });

            var response = await query.Execute();
            JObject result = response["redigerMedarbejder"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }

        public async Task<JObject> sletMedarbejder(string kortNummer)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("sletMedarbejder",
                        sletMedarbejder =>
                            sletMedarbejder
                                .Argument("kortnummer", "string", "kortnummer", true)
                                .Field("status")
                                .Field("error")


                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("kortnummer", kortNummer) });

            var response = await query.Execute();
            JObject result = response["sletMedarbejder"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }
        public async Task<JObject> opretBestilling(string VirksomhedsNavn, 
                                                   string virksomhedsAdresse, 
                                                   int virksomhedsTlfNummer, 
                                                   string virksomhedsKontaktPerson, 
                                                   string bestillingDato, 
                                                   string fristDato, 
                                                   int antal, 
                                                   string matriale, 
                                                   string karmHojde, 
                                                   string karmBrede, 
                                                   string rammeHojde, 
                                                   string rammeBrede)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("opretBestilling",
                        opretBestilling =>
                            opretBestilling
                                .Argument("virksomhedNavn", "string", "virksomhedNavn", true)
                                .Argument("virksomhedAdresse", "string", "virksomhedAdresse", true)
                                .Argument("virksomhedTlfnummer", "int", "virksomhedTlfnummer", true)
                                .Argument("virksomhedKontaktPerson", "string", "virksomhedKontaktPerson", true)
                                .Argument("bestilling", "string", "bestilling", true)
                                .Argument("frist", "string", "frist", true)
                                .Argument("antal", "int", "antal", true)
                                .Argument("matriale", "string", "matriale", true)
                                .Argument("karmHojde", "string", "karmHojde", true)
                                .Argument("karmBrede", "string", "karmBrede", true)
                                .Argument("rammeHojde", "string", "rammeHojde", true)
                                .Argument("rammeBrede", "string", "rammeBrede", true)
                                .Field("status")
                                .Field("error")


                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("virksomhedNavn", VirksomhedsNavn),
                    new GraphQLQueryArgument("virksomhedAdresse", virksomhedsAdresse),
                    new GraphQLQueryArgument("virksomhedTlfnummer", virksomhedsTlfNummer),
                    new GraphQLQueryArgument("virksomhedKontaktPerson", virksomhedsKontaktPerson), 
                    new GraphQLQueryArgument("bestilling", bestillingDato), 
                    new GraphQLQueryArgument("frist", fristDato), 
                    new GraphQLQueryArgument("antal", antal), 
                    new GraphQLQueryArgument("matriale", matriale), 
                    new GraphQLQueryArgument("karmHojde", karmHojde), 
                    new GraphQLQueryArgument("karmBrede", karmBrede), 
                    new GraphQLQueryArgument("rammeHojde", rammeHojde), 
                    new GraphQLQueryArgument("rammeBrede", rammeBrede) });

            var response = await query.Execute();
            JObject result = response["opretBestilling"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }

        public async Task<JObject> redigerProdukt(string produktid,
                                                   string frist,
                                                   string antal,
                                                   string matriale,
                                                   string karmHojde,
                                                   string karmBrede,
                                                   string rammeHojde,
                                                   string rammeBrede)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("redigerProdukt",
                        redigerProdukt =>
                            redigerProdukt
                                .Argument("produktId", "string", "produktId", true)
                                .Argument("frist", "string", "frist", true)
                                .Argument("antal", "int", "antal", true)
                                .Argument("matriale", "string", "matriale", true)
                                .Argument("karmHojde", "string", "karmHojde", true)
                                .Argument("karmBrede", "string", "karmBrede", true)
                                .Argument("rammeHojde", "string", "rammeHojde", true)
                                .Argument("rammeBrede", "string", "rammeBrede", true)
                                .Field("status")
                                .Field("error")


                    ),
                APIIP, arguments: new[] { 
                    new GraphQLQueryArgument("produktId", produktid),
                    new GraphQLQueryArgument("frist", frist),
                    new GraphQLQueryArgument("antal", antal),
                    new GraphQLQueryArgument("matriale", matriale),
                    new GraphQLQueryArgument("karmHojde", karmHojde),
                    new GraphQLQueryArgument("karmBrede", karmBrede),
                    new GraphQLQueryArgument("rammeHojde", rammeHojde),
                    new GraphQLQueryArgument("rammeBrede", rammeBrede) });

            var response = await query.Execute();
            JObject result = response["redigerProdukt"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }

        public async Task<JArray> Produkt()
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("produkt",
                        produkt =>
                            produkt
                                .Field("virksomhedNavn")
                                .Field("virksomhedAdresse")
                                .Field("virksomhedTlfnummer")
                                .Field("virksomhedKontaktPerson")
                                .Field("id")
                                .Field("kundeId")
                                .Field("bestilling")
                                .Field("frist")
                                .Field("antal")
                                .Field("matriale")
                                .Field("karmHojde")
                                .Field("karmBrede")
                                .Field("rammeHojde")
                                .Field("rammeBrede")
                                .Field("begyndt")
                                .Field("afsluttet")
            

                    ),
                APIIP
            );

            var response = await query.Execute();
            JArray result = response["produkt"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }
        public async Task<JArray> Steps(string produktId)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("steps",
                        steps =>
                            steps
                                .Argument("produktId", "string", "produktId", true)
                                .Field("id")
                                .Field("produktId")
                                .Field("navn")
                                .Field("medarbejderId")
                                .Field("start")
                                .Field("slut")

                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("produktId", produktId) }
            );

            var response = await query.Execute();
            JArray result = response["steps"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }

        public async Task<JObject> CheckInUdStep(string produktId, string kortnummer, string stepNavn)
        {
            var query = client.CreateQuery(builder =>
                    builder.Field("checkIndUdStep",
                        checkIndUdStep =>
                            checkIndUdStep
                                .Argument("produktId", "string", "produktId", true)
                                .Argument("kortnummer", "string", "kortnummer", true)
                                .Argument("stepNavn", "string", "stepNavn", true)
                                .Field("status")
                                .Field("error")

                    ),
                APIIP, arguments: new[] { new GraphQLQueryArgument("produktId", produktId), 
                    new GraphQLQueryArgument("kortnummer", kortnummer), 
                    new GraphQLQueryArgument("stepNavn", stepNavn) }
            );

            var response = await query.Execute();
            JObject result = response["checkIndUdStep"];
            return result;
            // builderResponse["verifiedLogin"]["verified"] ? "true" : "false";
        }



    }
}
