﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Models.DTOs.Requests;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoAppDBContext _dbContext;

        public TaskRepository(ToDoAppDBContext toDoAppDBContext)
        {
            _dbContext = toDoAppDBContext;
        }

        public async Task<TaskModel?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);           
        }

        public async Task<List<TaskModel>> FindAllTasksAsync()
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskModel?> SaveTaskAsync(TaskModel task)
        {
            if (task == null) 
                return null;

            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel?> UpdateTaskByIdAsync(TaskModel task, Guid id)
        {
            TaskModel? taskById = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (taskById == null)
                return null;

            taskById.Name = task.Name;
            taskById.Status = task.Status;
            taskById.Description = task.Description;

            var userExists = await _dbContext.Users.AnyAsync(x => x.Id == task.UserId);
            if (!userExists)
                return null;

            taskById.UserId = task.UserId;
            taskById.User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == task.UserId);

            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> DeleteTaskByIdAsync(Guid id)
        {
            TaskModel? taskById = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (taskById == null)
                return false;

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
