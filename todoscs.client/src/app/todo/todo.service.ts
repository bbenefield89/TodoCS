import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { FormGroup } from '@angular/forms';

type Todo = {
  id: string;
  title: string;
  isComplete: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl = "https://localhost:7002/todos"
  private _todos = signal<Todo[]>([])

  constructor(private http: HttpClient) { }

  fetchTodos() {
    this.http.get(this.apiUrl)
      .subscribe({
        error: console.error,

        next: (todos) => this._todos.set(todos as Todo[]),
      })
  }

  addTodo(todo: FormGroup) {
    this.http.post(this.apiUrl, todo.value)
      .subscribe({
        error: console.error,
        next: res => this._todos.set([...this._todos(), res as Todo]),
      })
  }

  completeTodo(todo: Todo) {
    this.http.put(`${this.apiUrl}/${todo.id}`, { ...todo, isComplete: true })
      .subscribe({
        error: console.error,

        next: res => {
          const todo = res as Todo
          const updatedTodos = this._todos().map(t => {
            if (t.id === todo.id) {
              t.title = todo.title
              t.isComplete = todo.isComplete
            }
            return t
          })

          this._todos.set(updatedTodos)
        }
      })
  }

  deleteTodo(id: string) {
    this.http.delete(`${this.apiUrl}/${id}`)
      .subscribe({
        error: console.error,
        next: () => {
          const updatedTodo = this._todos().filter(t => {
            return t.id !== id
          })

          this._todos.set(updatedTodo)
        }
      })
  }

  get todos() {
    return this._todos()
  }
}

