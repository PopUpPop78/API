import { Category } from "../category/category";
import { Currency } from "../currency/currency";

export class Expense {
    id!: number;
    amount!: number;
    date!: Date;
    description!: string;
    category!: Category;
    currency!: Currency;
}