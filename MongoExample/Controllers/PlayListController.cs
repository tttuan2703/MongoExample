using Microsoft.AspNetCore.Mvc;
using MongoExample.Contracts;
using MongoExample.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoExample.Controllers
{
    [Route("api/play-list")]
    [ApiController]
    public class PlayListController
    {
        private readonly IPlayListService _playListService;
        public PlayListController(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        [HttpGet]
        [Route("test")]
        public void Test()
        {
            _playListService.Init();
        }
    }
}
