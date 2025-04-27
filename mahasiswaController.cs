using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tpmodul10_103022300081.Models;

namespace tpmodul10_103022300081.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahasiswaController : ControllerBase
    {
        private static List<Mahasiswa> _mahasiswaList = new List<Mahasiswa>
        {
            new Mahasiswa("LeBron James", "1302000001"),
            new Mahasiswa("Stephen Curry", "1302000002"),
            new Mahasiswa("Kevin Durant", "1302000003"),
            new Mahasiswa("Subhan", "103022300081")
        };

        [HttpGet]
        public ActionResult<IEnumerable<Mahasiswa>> Get()
        {
            return Ok(_mahasiswaList);
        }

        [HttpGet("{index}")]
        public ActionResult<Mahasiswa> Get(int index)
        {
            if (index < 0 || index >= _mahasiswaList.Count)
            {
                return NotFound(new { message = "Mahasiswa tidak ditemukan" });
            }

            return Ok(_mahasiswaList[index]);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Mahasiswa mahasiswa)
        {
            if (string.IsNullOrEmpty(mahasiswa.Nama) || string.IsNullOrEmpty(mahasiswa.Nim))
            {
                return BadRequest(new { message = "Nama dan NIM tidak boleh kosong" });
            }

            _mahasiswaList.Add(new Mahasiswa(mahasiswa.Nama, mahasiswa.Nim));
            return CreatedAtAction(nameof(Get), new { index = _mahasiswaList.Count - 1 }, mahasiswa);
        }

        [HttpDelete("{index}")]
        public ActionResult Delete(int index)
        {
            if (index < 0 || index >= _mahasiswaList.Count)
            {
                return NotFound(new { message = "Mahasiswa tidak ditemukan" });
            }

            _mahasiswaList.RemoveAt(index);
            return Ok(new { message = "Data mahasiswa berhasil dihapus" });
        }
    }
}