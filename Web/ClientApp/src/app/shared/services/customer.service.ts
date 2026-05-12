import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    constructor(private dataService: DataService) { }

    addNewCustomer(newCustomer: Customer): Observable<boolean> {
        return this.dataService.post<boolean>('api/customer', newCustomer);
    }

    getAllCustomers(): Observable<Customer[]> {
        return this.dataService.get<Customer[]>('api/customer');
    }
}
