var Employee = function (employee) {
    var self = this;
    self.EmployeeId = ko.observable(employee.EmployeeId);
    self.Prefix = ko.observable(employee.Prefix);
    self.FirstName = ko.observable(employee.FirstName).extend({ required: true });
    self.MiddleInitial = ko.observable(employee.MiddleInitial);
    self.LastName = ko.observable(employee.LastName).extend({ required: true });
    self.FullName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });
    self.NameToPrintOnCheck = ko.observable(employee.NameToPrintOnCheck);
    self.Ssn = ko.observable(employee.Ssn).extend({ required: true });
    self.Address1 = ko.observable(employee.Address1);
    self.Address2 = ko.observable(employee.Address2);
    self.City = ko.observable(employee.City);
    self.State = ko.observable(employee.State);
    self.ZipCode = ko.observable(employee.ZipCode);
    //self.PhoneNumber = ko.observable(employee.PhoneNumer);
    //self.DateOfBirth = ko.observable(employee.DateOfBirth);
    self.PayRate = ko.observable(employee.PayRate).extend({ required: true });
    self.IsActive = ko.observable(employee.IsActive);
    self.Notes = ko.observable(employee.Notes);
    self.MaritalStatus = ko.observable(employee.MaritalStatus);
    self.Allowances = ko.observable(employee.Allowances);
    self.TaxForm = ko.observable(employee.TaxForm);
    self.Notes = ko.observable(employee.Notes);
    self.IsNew = ko.observable(employee.IsNew),
    self.DisplayName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });

    self.PopupTitle = ko.computed(function () {
        if (self.IsNew())
            return "Add Employee";

        return self.DisplayName();
    });


    self.errors = ko.validation.group(self);
    self.isValid = ko.computed(function () {
        return self.errors().length == 0;
    });
}

var Employees = function (dialogElement) {
    var employees = ko.observableArray();
    var selectedEmployee = ko.observable();
    var dialogElement = dialogElement;
    var maritalStatus = ["Married", "Single"];
    var taxForms = ["W2", "1099"];
    var states = [
    { name: 'ALABAMA', abbreviation: 'AL'},
    { name: 'ALASKA', abbreviation: 'AK'},
    { name: 'ARIZONA', abbreviation: 'AZ'},
    { name: 'ARKANSAS', abbreviation: 'AR'},
    { name: 'CALIFORNIA', abbreviation: 'CA'},
    { name: 'COLORADO', abbreviation: 'CO'},
    { name: 'CONNECTICUT', abbreviation: 'CT'},
    { name: 'DELAWARE', abbreviation: 'DE'},
    { name: 'FLORIDA', abbreviation: 'FL'},
    { name: 'GEORGIA', abbreviation: 'GA'},
    { name: 'HAWAII', abbreviation: 'HI'},
    { name: 'IDAHO', abbreviation: 'ID'},
    { name: 'ILLINOIS', abbreviation: 'IL'},
    { name: 'INDIANA', abbreviation: 'IN'},
    { name: 'IOWA', abbreviation: 'IA'},
    { name: 'KANSAS', abbreviation: 'KS'},
    { name: 'KENTUCKY', abbreviation: 'KY'},
    { name: 'LOUISIANA', abbreviation: 'LA'},
    { name: 'MAINE', abbreviation: 'ME'},
    { name: 'MARYLAND', abbreviation: 'MD'},
    { name: 'MASSACHUSETTS', abbreviation: 'MA'},
    { name: 'MICHIGAN', abbreviation: 'MI'},
    { name: 'MINNESOTA', abbreviation: 'MN'},
    { name: 'MISSISSIPPI', abbreviation: 'MS'},
    { name: 'MISSOURI', abbreviation: 'MO'},
    { name: 'MONTANA', abbreviation: 'MT'},
    { name: 'NEBRASKA', abbreviation: 'NE'},
    { name: 'NEVADA', abbreviation: 'NV'},
    { name: 'NEW HAMPSHIRE', abbreviation: 'NH'},
    { name: 'NEW JERSEY', abbreviation: 'NJ'},
    { name: 'NEW MEXICO', abbreviation: 'NM'},
    { name: 'NEW YORK', abbreviation: 'NY'},
    { name: 'NORTH CAROLINA', abbreviation: 'NC'},
    { name: 'NORTH DAKOTA', abbreviation: 'ND'},
    { name: 'OHIO', abbreviation: 'OH'},
    { name: 'OKLAHOMA', abbreviation: 'OK'},
    { name: 'OREGON', abbreviation: 'OR'},
    { name: 'PENNSYLVANIA', abbreviation: 'PA'},
    { name: 'RHODE ISLAND', abbreviation: 'RI'},
    { name: 'SOUTH CAROLINA', abbreviation: 'SC'},
    { name: 'SOUTH DAKOTA', abbreviation: 'SD'},
    { name: 'TENNESSEE', abbreviation: 'TN'},
    { name: 'TEXAS', abbreviation: 'TX'},
    { name: 'UTAH', abbreviation: 'UT'},
    { name: 'VERMONT', abbreviation: 'VT'},
    { name: 'VIRGINIA', abbreviation: 'VA'},
    { name: 'WASHINGTON', abbreviation: 'WA'},
    { name: 'WEST VIRGINIA', abbreviation: 'WV'},
    { name: 'WISCONSIN', abbreviation: 'WI'},
    { name: 'WYOMING', abbreviation: 'WY' }
    ];
    var init = function () {
        $.get("api/Employee/Get", "", function (result) {

            for (var i = 0; i < result.Employees.length; i++) {


                employees.push(new Employee(result.Employees[i]));

            }

            selectedEmployee(employees()[0]);
        });

    };

    var add = function () {
        var employee = new Employee({
            EmployeeId: 0,
            Prefix: '',
            FirstName: '',
            LastName: '',
            MiddleInitial: '',
            NameToPrintOnCheck: '',
            Ssn: '',
            Address1: '',
            Address2: '',
            City: '',
            State: 'TX',
            ZipCode: '',
            PhoneNumber: '',
            DateOfBirth: '',
            PayRate: 0.00,
            IsActive: true,
            Allowances: 0,
            MaritalStatus: '',
            TaxForm: 'W2',
            Notes: '',
            IsNew: true

        });

        selectedEmployee(employee);

    }

    var edit = function (employee) {
        selectedEmployee(employee);
    };

    var save = function () {

        if (selectedEmployee().isValid()) {


            var employee = ko.mapping.toJS(selectedEmployee);

            var urlData = employee.IsNew ? { url: "api/Employee/Post", type: "Post" } : { url: "api/Employee/Put", type: "PUT" }

            //var dto = { employee: ko.mapping.toJS(selectedEmployee) }

            $.ajax({
                url: urlData.url,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(employee),
                type: urlData.type,
                success: function (result) {
                    notification.show(result);
                    $(dialogElement).modal("hide");
                    if (employee.IsNew) {
                        selectedEmployee().IsNew(false);
                        employees.push(selectedEmployee());

                    }
                }
            });
        }
        else {
            selectedEmployee().errors.showAllMessages();
        }
    };

    var remove = function (employee) {

        alertify.confirm("Are you sure you want to delete " + employee.DisplayName() + "?", function (e) {
            if (e) {


                $.ajax({
                    url: "api/Employee/Delete/" + employee.EmployeeId(),
                    type: "DELETE",
                    success: function (result) {
                        notification.show(result);
                        if (result.IsValid)
                            employees.remove(employee);

                    }
                });

            }
        });
    }

    init();

    return {
        add: add,
        edit: edit,
        employees: employees,
        martialStatus: maritalStatus,
        remove: remove,
        save: save,
        selectedEmployee: selectedEmployee,
        states: states,
        taxForms: taxForms
    }
};