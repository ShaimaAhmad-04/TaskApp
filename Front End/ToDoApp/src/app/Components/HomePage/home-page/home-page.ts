import { Component, ElementRef, ViewChild } from '@angular/core';
import { ReactiveFormsModule, FormGroup, Validators, FormControl } from '@angular/forms';
import { Task } from '../../../../Interfaces/iTask';
import { CommonModule, DatePipe } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-home-page',
  imports: [NgxPaginationModule, ReactiveFormsModule, CommonModule],
  providers: [DatePipe],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css'
})
export class HomePage {

  @ViewChild('closeAddModal') closeAddModal: ElementRef | undefined;

  constructor(private _datePipe: DatePipe) { }

  Priorities = [
    { id: 1, name: "High" },
    { id: 2, name: "Medium" },
    { id: 3, name: "Low" }
  ];


  Status = [
    { id: 0, name: "In progress" },
    { id: 1, name: "Completed" }

  ]

  paginationConfig = { itemsPerPage: 3, currentPage: 1 };

  Tasks: Task[] = [
    {
      Id: 1,
      Name: "study algo",
      Description: "Review algorithms chapters and solve practice problems.",
      DeadLine: new Date(2025, 8, 27), // September
      PriorityId: this.Priorities[1].id,
      PriorityName: this.Priorities[1].name,
      IsComplete: true
    },
    {
      Id: 2,
      Name: "clean the house",
      Description: "Vacuum, dust, and organize all rooms.",
      DeadLine: new Date(2025, 8, 27), // September
      PriorityId: this.Priorities[2].id,
      PriorityName: this.Priorities[2].name,
      IsComplete: false
    },
    {
      Id: 3,
      Name: "grocery shopping",
      Description: "Buy vegetables, fruits, and other essentials.",
      DeadLine: new Date(2025, 8, 27), // September
      PriorityId: this.Priorities[0].id,
      PriorityName: this.Priorities[0].name,
      IsComplete: true
    },
    {
      Id: 4,
      Name: "pay electricity bill",
      Description: "Pay the electricity bill online before due date.",
      DeadLine: new Date(2025, 8, 27), // September
      PriorityId: this.Priorities[2].id,
      PriorityName: this.Priorities[2].name,
      IsComplete: false
    },
    {
      Id: 5,
      Name: "prepare presentation",
      Description: "Create slides for Monday's meeting.",
      DeadLine: new Date(2025, 8, 27), // September
      PriorityId: this.Priorities[0].id,
      PriorityName: this.Priorities[0].name,
      IsComplete: false
    }
  ];

  addTaskForm: FormGroup = new FormGroup({
    Id: new FormControl(null),
    Name: new FormControl(null, [Validators.required]),
    Description: new FormControl(null),
    DeadLine: new FormControl(null),
    PriorityId: new FormControl(null),
    IsComplete: new FormControl(false)
  })


  addEditTask(taskId?: Number) {


    if (taskId == null) {

      if (this.addTaskForm.valid) {

        // add a new task
        let newTask: Task = {
          Id: this.Tasks.length + 1,
          Name: this.addTaskForm.get("Name")?.value,
          Description: this.addTaskForm.get("Description")?.value,
          IsComplete: false,
          DeadLine: this.addTaskForm.get("DeadLine")?.value,
          PriorityId: this.addTaskForm.get("PriorityId")?.value,
        }
        this.Tasks.push(newTask)
        this.closeAddModal?.nativeElement.click()
        this.clearAddForm()
      }
    }
    else {
      //edit task
      let taskToEdit = this.Tasks.find(x => x.Id === taskId)

      if (taskToEdit) {
        taskToEdit.Name = this.addTaskForm.get('Name')?.value;
        taskToEdit.Description = this.addTaskForm.get('Description')?.value;
        taskToEdit.DeadLine = this.addTaskForm.get('DeadLine')?.value;
        taskToEdit.PriorityId = this.addTaskForm.get('PriorityId')?.value;
        taskToEdit.PriorityName = this.Priorities[this.addTaskForm.get('PriorityId')?.value - 1].name,
          taskToEdit.IsComplete = this.addTaskForm.get('IsComplete')?.value;
      }
      this.closeAddModal?.nativeElement.click()
      this.clearAddForm()
    }
  }


  PatchForm(taskId?: Number) {
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

  finishTask(taskId: Number) {
    var doneTask = this.Tasks.find(x => x.Id === taskId)

    if (doneTask)
      doneTask.IsComplete = !doneTask.IsComplete

  }

  deleteTask(taskId: Number) {
    var delTask = this.Tasks.find(x => x.Id === taskId)

    if (delTask)
    {
      let index = this.Tasks.indexOf(delTask)
       this.Tasks.splice(index,1)
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
