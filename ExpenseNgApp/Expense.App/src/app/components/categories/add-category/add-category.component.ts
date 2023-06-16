import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category.service';
import { NavigationService } from 'src/app/services/navigation.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  fg!: FormGroup;
  isAdding: boolean = false;

  constructor(private cats: CategoryService,
    private toast: ToastrService,
    private nav: NavigationService,
    private fb: FormBuilder) {

  }
  ngOnInit(): void {
    this.fg = this.fb.group({
      name:['', Validators.required],
      description:['']
    })
  }

  addCategory() {
    if(this.fg.valid){
      this.isAdding = true;
      const cat = this.fg.value;
      this.cats.add(cat).subscribe({
        next: res => {
          this.nav.back();
          this.toast.success(`Successfully added category ${cat.name}`)
        },
        error: err => {
          console.log(err);
          this.toast.error('An unknown error occurred');
        }
      })
      .add(() => this.isAdding = false);
    }
  }

}
