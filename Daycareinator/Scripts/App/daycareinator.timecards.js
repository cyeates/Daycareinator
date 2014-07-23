var Timecard = function (timecard) {
    var self = this;
    self.EmployeeId = timecard.EmployeeId;
    self.Name = timecard.Name;
    self.Last4Ssn = timecard.Last4Ssn;
    self.RegularPayRateDisplay = numeral(timecard.RegularPayRate).format('$0,0.00');
    self.OtPayRateDisplay = numeral(timecard.OtPayRate).format('$0,0.00');
    self.TimecardEntries = ko.observableArray();

    for (var i = 0; i < timecard.TimecardEntries.length; i++) {
        self.TimecardEntries.push(new Entry(timecard.TimecardEntries[i]));
    }

    self.TotalHours = ko.computed(function () {
        var total = 0;
        for (var i = 0; i < self.TimecardEntries().length; i++) {
            if (self.TimecardEntries()[i].Hours() != null)
                var value = parseFloat(self.TimecardEntries()[i].Hours());
            if (!isNaN(value))
                total += value;
            else {

                self.TimecardEntries()[i].Hours(0);
            }
        }

        return numeral(total).format("0.00");
    });

    self.RegularHours = ko.computed(function () {
        var totalHours = self.TotalHours();
        return numeral(totalHours >= 40 ? 40 : totalHours).format("0.00");
    });

    self.OtHours = ko.computed(function () {
        var totalHours = self.TotalHours();
        return numeral(totalHours <= 40 ? 0 : totalHours - 40).format("0.00");
    });

    self.GrossPay = ko.computed(function () {
        var total = (timecard.RegularPayRate * self.RegularHours()) + (timecard.OtPayRate * self.OtHours());
        return numeral(total).format('$0,0.00');
    });
};

var Entry = function (entry) {
    this.Date = entry.Date;
    this.Hours = ko.observable(entry.Hours);
};

var DateModel = function (date) {
    this.dateHtml = date.DisplayString + "<br/>" + date.DayOfWeek;
    
}

var Timecards = function () {

    var timecards = ko.observableArray();
    var timecardDate = ko.observable(); //date for date picker
    var isTimecardClosed = ko.observable();
    var timecardDates = ko.observableArray(); //dates in table
    var isLoaded = ko.observable(false); //hack to prevent timecardDate subscribe from firing again when it loads
    var init = function () {
        $.get("api/Timecard/Get", "", function (result) {
            loadTimecard(result);
                       
        });

        

    };

    var save = function () {
        var timecardDto = [];
        for (var i = 0; i < timecards().length ; i++) {
            timecardDto.push({ EmployeeId: timecards()[i].EmployeeId, TimecardEntries: ko.mapping.toJS(timecards()[i].TimecardEntries()) });
        }

        var dateDto = { Date: timecardDate() };
        var dto = {EmployeeTimecards: timecardDto, Date: dateDto };

        $.ajax({
            url: "api/Timecard/Save",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dto),
            type: "POST",
            success: function (result) {
                notification.show(result);
            }
        });

    };

    var submit = function () {
        alertify.confirm("Are you sure you want to submit and close this timecard? You will not be able to make changes after you submit.", function (e) {
            if (e) {
                var timecardDto = [];
                for (var i = 0; i < timecards().length ; i++) {
                    timecardDto.push({ EmployeeId: timecards()[i].EmployeeId, TimecardEntries: ko.mapping.toJS(timecards()[i].TimecardEntries()) });
                }

                var dateDto = { Date: timecardDate() };
                var dto = { EmployeeTimecards: timecardDto, Date: dateDto };

                $.ajax({
                    url: "api/Timecard/Submit",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(dto),
                    type: "POST",
                    success: function (result) {
                        if (result.IsValid) {
                            isTimecardClosed(true);
                        }
                        notification.show(result);
                    }
                });
            }

        });
        
    };

    timecardDate.subscribe(function (date) {
        if (isLoaded()){
            changeDate(date);
        }
    });

    var changeDate = function (date) {
        isLoaded(false);
        $.ajax({
            url: "api/Timecard/ChangeDate",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Date: date }),
            type: "POST",
            success: function (result) {
                loadTimecard(result);
            }
        });
        
    };


    var loadTimecard = function (result) {
        timecardDate(result.Dates[0].Date);
        isTimecardClosed(result.IsTimecardClosed);
        timecards([]);
        for (var i = 0; i < result.EmployeeTimecards.length; i++) {
            timecards.push(new Timecard(result.EmployeeTimecards[i]));
        }

        timecardDates([]);
        for (var i = 0; i < result.Dates.length; i++) {
            timecardDates.push(new DateModel(result.Dates[i]));
        }

        isLoaded(true);
        console.log("timecard date=" + timecardDate());
    };

    init();

    return {
        isTimecardClosed: isTimecardClosed,
        timecards: timecards,
        timecardDate: timecardDate,
        timecardDates: timecardDates,
        save: save,
        submit: submit,
        changeDate: changeDate,
        
    };
};