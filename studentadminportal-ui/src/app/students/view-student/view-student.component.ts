import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, TitleStrategy } from '@angular/router';
import { Gender } from 'src/app/models/ui-models/genderui.model';
import { Student } from 'src/app/models/ui-models/studentui.model';
import { GenderService } from 'src/app/services/gender.service';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {
  studentId: string | null | undefined;
  // from UI models
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    profileImageUrl: '',
    gender: {
      id: '',
      description: ''
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: ''
    }
  }
  isNewStudent = false;
  header = '';
  displayProfileImageUrl = '';
  genderList: Gender[] = []

  @ViewChild('studentDetailsForm') studentDetailsForm?: NgForm;

  // si activated route yung kukuha ng parameters na galing sa route??
  constructor(private readonly studentService: StudentService, private readonly route: ActivatedRoute, private readonly genderService: GenderService, private snackbar: MatSnackBar, private router: Router) { }

  // paramMap is to read the route??
  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        // same name sa id na nasa app-routing module tapos i-assign mo yung makukuhang id sa studentId property na ginawa mo sa taas
        this.studentId = params.get('id');

        if(this.studentId) {
          // check if route contains keyword 'Add' para ma-determine kung mag-add siya or mag-update
          if(this.studentId.toLowerCase() === 'Add'.toLowerCase()) {
            // -> new Student Functionality
            this.isNewStudent = true;
            this.header = 'Add New Student';
            // add default image to new student
            this.setImage();

          } else {
            // -> Existing Student Functionality
            this.isNewStudent = false;
            this.header = 'Edit Student';

            this.studentService.getStudent(this.studentId).subscribe(
              (successResponse) => {
                // para magamit mo sa html file
                this.student = successResponse;
                this.setImage();
              },
              (errorResponse) => {
                this.setImage();
              }
            );
          }

          // since need ng both add and update yung gender, pwedeng nasa labas lang siya
          this.genderService.getGenderList().subscribe(
            successResponse => {
              this.genderList = successResponse;
            }
          );
        }
      }
    );
  }

  onUpdate(): void {

    if (this.studentDetailsForm?.form.valid) {
      // Call Student Service to Update Student
      this.studentService.updateStudent(this.student.id, this.student).subscribe(
        (successResponse) => {
          // Show a notification
          this.snackbar.open('Changes have been saved', undefined, {
            duration: 2000
          });
        }
      );
    } else {
      this.snackbar.open('Fill the required fields.', undefined, {
        duration: 2000
      });
    };
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id).subscribe(
      (successResponse) => {
        this.router.navigateByUrl('students').then(() => {
          this.snackbar.open('Record has been deleted', undefined, {
            duration: 2000
          });
      })
      },
      (errorResponse) => {
        // Log it
      }
    )
  }

  onAdd(): void {
    // check if form is valid
    if (this.studentDetailsForm?.form.valid) {
      this.studentService.addStudent(this.student).subscribe(
        (successResponse) => {
          // if you want to redirect it to the student detail, use `students/${successRespose.id}` as URL take note of ``
          this.router.navigateByUrl('students').then(() => {
            this.snackbar.open('Record has been added', undefined, {
              duration: 2000
            });
          })
        },
        (errorResponse) => {
          console.log(errorResponse);
        }
      )
    } else {
      this.snackbar.open('Fill the required fields.', undefined, {
        duration: 2000
      });
    };
  }

  uploadImage(event: any): void {
    if(this.studentId) {
      const file: File = event.target.files[0];
      this.studentService.uploadImage(this.studentId, file).subscribe(
        (successResponse) => {
          this.student.profileImageUrl = successResponse;
          this.setImage();

          this.snackbar.open('Image has been uploaded succesfully', undefined, {
            duration: 2000
          });
        }
      );
    }
  }

  private setImage(): void {
    if(this.student.profileImageUrl) {
      // Fetch the image by URL
      this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImageUrl);
    } else {
      // Display default image
      this.displayProfileImageUrl = '/assets/user-icon.jpg';
    }
  }
}
