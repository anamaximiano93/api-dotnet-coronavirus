using DIO.ApiCoronavirus.Data.Collections;
using DIO.ApiCoronavirus.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIO.ApiCoronavirus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController:ControllerBase
    {
        readonly Data.MongoDB mongoDB;

        IMongoCollection<Infected> mongoCollection;

        public InfectedController(Data.MongoDB mongoDB)
        {
            try
            {
                this.mongoDB = mongoDB;
                mongoCollection = mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());

            }
            catch (Exception ex)
            {

                throw new MongoException(ex.Message, ex);
            }

        }
        [HttpPost]
        public ActionResult SaveInfected([FromBody] DTOInfected dTO)
        {
            try
            {

            var infected = new Infected(dTO.BirthDate, dTO.Sex, dTO.Latitude, dTO.Longitude);

            mongoCollection.InsertOne(infected);

            return StatusCode(201, "Infected add with successed");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message.ToJson());
            }
        } 
     
        [HttpPut("{id}")]
        public ActionResult UpdateInfected( string id,[FromBody] DTOInfected dTO)
        {
            try
            {
            var infected = new Infected(id, dTO.BirthDate, dTO.Sex, dTO.Latitude, dTO.Longitude);

            var filter = Builders<Infected>.Filter.Eq( "_id",infected.Id);
            var update = Builders<Infected>.Update.Set(i => i.BirthDate, infected.BirthDate)
                .Set(i => i.Sex, infected.Sex)
                .Set(i => i.Localization, infected.Localization);

           var options = new FindOneAndUpdateOptions<Infected>();
           options.IsUpsert = false;
           options.ReturnDocument = ReturnDocument.After;


            var result = mongoCollection.FindOneAndUpdate(filter, update, options);

            return StatusCode(200,  result);

            }
            catch (Exception ex)
            {

            
            return StatusCode(500,  ex.Message.ToJson());  
            }


        }     
        
        [HttpDelete("{id}")]
        public ActionResult DeleteInfected( string id)
        {
            try
            {
            var filter = Builders<Infected>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = mongoCollection.FindOneAndDelete(filter);

            return StatusCode(200,  result);

            }
            catch (Exception ex)
            {

            return StatusCode(500,  ex.Message.ToJson());
             
            }

        }

        [HttpGet]
        public ActionResult GetInfected()
        {
            try
            {
            var result = new List<DTOInfected>();
            List<Infected> infecteds = mongoCollection.Find(Builders<Infected>.Filter.Empty).ToList();

            foreach(var item in infecteds)
            {
                var itemDTO = new DTOInfected();
                itemDTO.ID = Convert.ToString(item.Id);
                itemDTO.BirthDate = item.BirthDate;
                itemDTO.Sex = item.Sex;
                itemDTO.Latitude = item.Localization.Latitude;
                itemDTO.Longitude = item.Localization.Longitude;

                result.Add(itemDTO);
            }       

            return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message.ToJson()) ; 
            }
        } 
        [HttpGet("{id}")]
        public ActionResult GetInfected(string id)
        {
            try
            {

                var filter = Builders<Infected>.Filter.Eq("_id", ObjectId.Parse(id));
                var infected = mongoCollection.Find(filter).First();
                var itemDTO = new DTOInfected();
                itemDTO.ID = Convert.ToString(infected.Id);
                itemDTO.BirthDate = infected.BirthDate;
                itemDTO.Sex = infected.Sex;
                itemDTO.Latitude = infected.Localization.Latitude;
                itemDTO.Longitude = infected.Localization.Longitude;

                return Ok(itemDTO);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToJson());
                     
            }


        }

       
       
    }
}
