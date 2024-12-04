using server.Data;
using server.Dtos;
using server.Interfaces;
using server.Models;
using server.Utlis;

namespace server.Services
{
    public class InstructionService(AppDbContext dbContext,IImageStorageService imageStorageService) : IInstructionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="loggedUser"></param>
        /// <returns></returns>
        /// <exception cref="MediaFileNotFoundException"></exception>
        public async Task<Instruction> Create(InstructionDto dto, User loggedUser)
        {
            var media = await dbContext.MediaFiles.FindAsync(dto.Media.Id) ?? throw new MediaFileNotFoundException();

            var instruction = new Instruction
            {
                Description = dto.Description,
                Position = dto.Position,
                Media = media
            };
            var persisted = dbContext.Instructions.Add(instruction);
            return persisted.Entity;
        }

        public Instruction Delete(Instruction instruction)
        {
            if (instruction.Media is not null)
            {
                imageStorageService.DeleteImage(instruction.Media);
            }

            dbContext.Instructions.Remove(instruction);
            return instruction;
        }
    }
}
