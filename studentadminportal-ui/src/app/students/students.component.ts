import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, _MatTableDataSource } from '@angular/material/table';
import { Student } from '../models/ui-models/studentui.model';
import { StudentService } from './student.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  // make sure that you import the right model from UI model not API
  students: Student[] = [];
  // from angular material documentation
  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'email', 'mobile', 'gender', 'edit'];
  dataSource: MatTableDataSource<Student> = new MatTableDataSource<Student>();
  // to link paginator with table
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString = '';

  // inject student service here to make http calls
  constructor(private studentService: StudentService) { }

  ngOnInit(): void {
    // Fetch students
    // to use the observable, you need to subscribe to it
    this.studentService.getStudents().subscribe(
      (successResponse) => {
        // the result from the successResponse (the data from the API) will be passed on to this.students which is linked on the Student ui model
        this.students = successResponse;
        this.dataSource = new MatTableDataSource<Student>(this.students);

        if (this.matPaginator) {
          this.dataSource.paginator = this.matPaginator;
        }

        if (this.matSort) {
          this.dataSource.sort = this.matSort;
        }
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  filterStudents() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }

}
