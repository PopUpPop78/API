import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  categories: Category[] = [];

  constructor(private cats: CategoryService, private toast: ToastrService, private route: Router, private activeRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.cats.getAll().subscribe({
      next: res => {
        this.categories = res;
      },
      error: err => {
        console.log(err);
        this.toast.error("An unknown error occurred");
      }
    });
  }

  update(cat: Category) {
    this.route.navigate(['update-category/' + cat.id], {relativeTo: this.activeRoute});
  }

  addCategory() {
    this.route.navigate(['add-category'], {relativeTo: this.activeRoute});
  }

}
