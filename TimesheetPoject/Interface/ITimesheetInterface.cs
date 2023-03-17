﻿using Microsoft.AspNetCore.Mvc;
using TimesheetPoject.Model;

namespace TimesheetPoject.Interface
{
    public interface ITimesheetInterface
    {
        public IActionResult Regester(RegistrationModel regestrationModel);
        public IActionResult Login(LoginModel loginModel);
        public IActionResult ResetPassword(LoginModel loginModel);


    }
}