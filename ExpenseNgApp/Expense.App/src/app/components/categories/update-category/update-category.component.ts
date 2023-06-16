import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category/category';
import { CategoryService } from 'src/app/services/category.service';
import { NavigationService } from 'src/app/services/navigation.service';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrls: ['./update-category.component.css']
})
export class UpdateCategoryComponent implements OnInit {
  
  fg!: FormGroup;
  isUpdating: boolean = false;
  cat!: Category;

  constructor(private cats: CategoryService,
    private toast: ToastrService,
    private activeRoute: ActivatedRoute,
    private route: Router,
    private fb: FormBuilder,
    private nav: NavigationService) {

      this.fg = this.fb.group({
        id: [''],
        name: [''],
        description: ['']
      });

  }
  ngOnInit(): void {

    this.activeRoute.paramMap.subscribe({
      next: res => {
        const id = +res.get('id')!;
        this.cats.get(id).subscribe({
          next: res => {
            this.cat = res;
            this.fg = this.fb.group({
              id: [this.cat.id],
              name: [this.cat.name],
              description: [this.cat.description]
            })
          },
          error: err => {
            console.log(err);
            this.toast.error('An unknown error occurred');
          }
        })
      }
    })
  }

  updateCategory() {
    if(this.fg.valid){
      this.isUpdating = true;
      const cat = this.fg.value;
      this.cats.update(cat).subscribe({
        next: res => {
          setTimeout(() => {
            this.route.navigate([`dashboard/categories`]);            
          }, 0);
        },
        error: err => {
          console.log(err);
          this.toast.error('An unknown error occurred');
        }
      })
      .add(() => this.isUpdating = false);
    }
  }

}
