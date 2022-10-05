import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from './students/students.component';
import { ViewStudentComponent } from './students/view-student/view-student.component';

const routes: Routes = [
  {
    // this means that if i open the home page (hence, path is empty), i want to display the students component
    path: '',
    component: StudentsComponent
  }, // to create another path just add another {}
  // path cannot start with a slash
  {
    path: 'students',
    component: StudentsComponent
  },
  {
    // syntax when u wanna access a student detail through id
    path: 'students/:id',
    component: ViewStudentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
