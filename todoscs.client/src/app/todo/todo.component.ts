import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TodoService } from './todo.service';

type Todo = {
  id: string;
  title: string;
  isComplete: boolean;
}

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrl: './todo.component.css',
})
export class TodoComponent implements OnInit {
  todoForm: FormGroup

  constructor(public todoService: TodoService, private formBuilder: FormBuilder) {
    this.todoForm = this.formBuilder.group({ title: "" })
  }

  ngOnInit(): void {
    this.todoService.fetchTodos()
  }

  onSubmit() {
    this.todoService.addTodo(this.todoForm)
    this.todoForm.reset()
  }

  handleCompleteTodo(todo: Todo) {
    this.todoService.completeTodo(todo)
  }

  handleDeleteTodo(id: string) {
    this.todoService.deleteTodo(id)
  }

}
