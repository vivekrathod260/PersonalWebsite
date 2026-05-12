import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from './shared/services/customer.service';
import { Customer } from './shared/models/customer'

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    customerForm: FormGroup = new FormGroup({});
    customers: Customer[] = [];

    constructor(private formBuilder: FormBuilder, private customerService: CustomerService) { }

    ngOnInit(): void {
        this.customerForm = this.formBuilder.group({
            fullName: ['', Validators.required],
            email: ['', Validators.email]
        });

        this.customerService.getAllCustomers().subscribe(res => {
            this.customers = res;
        });
    }

    onSubmit() {
        if (this.customerForm.valid) {
            let newCustomer: Customer = {
                fullName: this.customerForm.get('fullName')?.value,
                email: this.customerForm.get('email')?.value
            }

            this.customerService.addNewCustomer(newCustomer).subscribe(res => console.log("Added"));

            this.customerService.getAllCustomers().subscribe(res => {
                this.customers = res;
            });
        }
    }
}
