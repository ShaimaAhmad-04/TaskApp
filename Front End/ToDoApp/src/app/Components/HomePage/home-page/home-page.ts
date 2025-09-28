import { Component, ElementRef, ViewChild } from '@angular/core';
import { ReactiveFormsModule, FormGroup, Validators, FormControl } from '@angular/forms';
import { Task } from '../../../../Interfaces/iTask';
import { CommonModule, DatePipe } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { TasksServices } from '../../../../services/tasks-services';
import { List } from '../../../../Interfaces/iList';
import { LookUpServices } from '../../../../services/look-up-services';
import { LookupsMajorCodes } from '../../../enums/enums';
import { routes } from '../../../app.routes';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  imports: [NgxPaginationModule, ReactiveFormsModule, CommonModule],
  providers: [DatePipe],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css'
})
export class HomePage {

  @ViewChild('closeAddModal') closeAddModal: ElementRef | undefined;

  constructor(private _datePipe: DatePipe,
    private _taskService: TasksServices,
    private _lookupService: LookUpServices,
  ) { }



  ngOnInit() {
    this.loadTasks()
    this.loadPriorities()

  }

  Priorities: List[] = [];


  Status = [
    { id: 0, name: "In progress" },
    { id: 1, name: "Completed" }

  ]

  paginationConfig = { itemsPerPage: 3, currentPage: 1 };

  Tasks: Task[] = [];

  loadTasks() {
    this.Tasks = []
    this._taskService.getAll().subscribe(
      {
        next: (res: any) => {
          if (res.length > 0) {
            res.forEach((task: any) => {
              let newTask: Task = {
                Id: task.id,
                Name: task.name,
                Description: task.description,
                PriorityId: task.priorityId,
                PriorityName: task.priorityName,
                DeadLine: task.deadline,
                IsComplete: task.isComplete
              }
              this.Tasks.push(newTask)
            });
          }
        }
      }
    )

  }

  loadPriorities() {
    this.Priorities = [
      { Id: null, Name: "choose priority" }
    ]

    this._lookupService.getPrios(LookupsMajorCodes.priorities).subscribe({
      next: (res: any) => {
        if (res?.length > 0) {
          res.forEach((prio: any) => {
            this.Priorities.push({ Id: prio.id, Name: prio.name })
          })
        }
      },
      error: err => console.log(err.message)
    })

  }

  addTaskForm: FormGroup = new FormGroup({
    Id: new FormControl(null),
    Name: new FormControl(null, [Validators.required]),
    Description: new FormControl(null),
    DeadLine: new FormControl(null),
    PriorityId: new FormControl(null),
    IsComplete: new FormControl(false)
  })


  addEditTask() {

    let taskId = this.addTaskForm.value.Id ?? 0;

    let task: Task = {
      Id: taskId,
      Name: this.addTaskForm.value.Name,
      Description: this.addTaskForm.value.Description,
      DeadLine: this.addTaskForm.value.DeadLine,
      PriorityId: this.addTaskForm.value.PriorityId,
      IsComplete: this.addTaskForm.value.IsComplete
    };

    if (this.addTaskForm.valid) {
      if (taskId === 0) {
        // ✅ Add
        this._taskService.add(task).subscribe({
          next: res => {
            this.loadTasks();
            this.closeAddModal?.nativeElement.click();
            this.clearAddForm();
          },
          error: err =>
            console.log(err.error?.message ?? err.error ?? "Unexpected Error")
        });
      } else {
        // ✅ Update
        this._taskService.update(task).subscribe({
          next: res => {
            this.loadTasks();
            this.closeAddModal?.nativeElement.click();
            this.clearAddForm();
          },
          error: err => console.log(err.error?.message ?? err.message)
        });
      }
    }
  }



  PatchForm(taskId?: number) {
    // this.addEditTask(taskId)
    let taskToEdit = this.Tasks.find(x => x.Id === taskId)

    this.addTaskForm.patchValue({
      Id: taskToEdit?.Id,
      Name: taskToEdit?.Name,
      Description: taskToEdit?.Description,
      DeadLine: this._datePipe.transform(taskToEdit?.DeadLine, 'yyyy-MM-dd'),
      PriorityId: taskToEdit?.PriorityId,
      IsComplete: taskToEdit?.IsComplete
    })
  }
  // connnnect with backendddd
  finishTask(taskId: number) {
    var doneTask = this.Tasks.find(x => x.Id === taskId)

    if (doneTask)
      doneTask.IsComplete = !doneTask.IsComplete

  }

  deleteTask(taskId: number) {
    var delTask = this.Tasks.find(x => x.Id === taskId)

    if (delTask) {
      this._taskService.delete(taskId).subscribe({
        next: res => this.loadTasks(),
        error: err => console.log(err.messages)
      })
    }
  }


  clearAddForm() {
    this.addTaskForm.reset()
  }

  openModal(id: number) {
    //find the task based on its id 
  }

  changePage(pageNumber: number) {
    this.paginationConfig.currentPage = pageNumber
  }

}
