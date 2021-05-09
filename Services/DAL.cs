using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SverigeVader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

namespace SverigeVader.Services
{
    public class DAL : IDAL
    {
        private readonly IConfiguration _configuration;
        private string _myMongoDBSettings;
        public DAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _myMongoDBSettings = _configuration["MyMongoDBSettings"];
        }
        private MongoClient GetClient()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(_configuration[$"{_myMongoDBSettings}:Host"], 10255);
            settings.UseTls = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            settings.RetryWrites = false;

            MongoIdentity identity = new MongoInternalIdentity(_configuration[$"{_myMongoDBSettings}:DbNaMe"], _configuration[$"{_myMongoDBSettings}:UserName"]);
            MongoIdentityEvidence evidence = new PasswordEvidence(_configuration[$"{_myMongoDBSettings}:Password"]);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);

            return client;
        }

        public IMongoCollection<Measurement> MeasurementCollection()
        {
            var client = GetClient();
            var database = client.GetDatabase(_configuration[$"{_myMongoDBSettings}:dbName"]);
            var measurementCollection = database.GetCollection<Measurement>(_configuration[$"{_myMongoDBSettings}:collectionName"]);
            return measurementCollection;
        }

    }
}
