using System;

interface IPayment
{
    void Pay(double amount);
    void Refund(double amount);
}