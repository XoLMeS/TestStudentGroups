function Group(data) {
    var self = this;

    self.id = data.id;
    self.title = ko.observable(data.title);
    self.number = ko.observable(data.number);
    self.capacity = ko.observable(data.capacity);
    self.maxcap = ko.observable(data.maxcap);
}

function Student(data) {
    var self = this;

    self.id = data.id;
    self.name = ko.observable(data.name);
    self.surname = ko.observable(data.surname);
    self.average_score = ko.observable(data.average_score);
    self.birthday = ko.observable(data.birthday);
    self.exp = ko.observable(data.exp);
}



function TestViewModel() {
    var self = this;

    self.title = ko.observable("Groups");
   

    // Group info
    self.AllGroups = ko.observable();
    self.GroupId = ko.observable();
    self.GroupData = ko.observable(null);

    // Group form
    self.newTitle = ko.observable("");
    self.newNumber = ko.observable("");
    self.newMaxcap = ko.observable("");

    // Student info
    self.StudentId = ko.observable();
    self.StudentData = ko.observable();

    // Student form
    self.newName = ko.observable("");
    self.newSurname = ko.observable("");
    self.newAverage_score = ko.observable("");
    self.newBirthday = ko.observable("");
    self.newExp = ko.observable("");

    // Arrays
    self.groups = ko.observableArray([]);
    self.students = ko.observableArray([]);
    self.allStudents = ko.observableArray([]);

    // Behaviours    
    self.goToGroup = function (group) { location.hash = 'groups/students/' + group.id };

    self.newGroup = function () {
        // Post to server
        $.ajax("/group/create", {
            data: ko.toJSON({
                title: self.newTitle(),
                number: self.newNumber(),
                maxcap: self.newMaxcap(),
                capacity: 0
            }),
            type: "post", contentType: "application/json",
            success: function (result) {
                // Add to list
                self.groups.push(new Group({ title: result.title, number: result.number, maxcap: result.maxcap, capacity: 0 }));    
            }
        });
        // Clear form
        self.newTitle("");
        self.newNumber("");
        self.newMaxcap("");
    };

    self.loadStudents = function () {

        $.getJSON("/student", function (array) {
            console.log("getStudents");
            var mappedStudents = $.map(array, function (item) { return new Student(item) });
            self.allStudents([]);
        });

    }

    self.sortGroups = function (str) {
        if (str == "title") {
            self.groups.sort(function (left, right) {
                return left.title() == right.title() ? 0 : (left.title() < right.title() ? -1 : 1)
            });
        }
        if (str == "number") {
            self.groups.sort(function (left, right) {
                return left.number() - right.number();
            });
        }
        if (str == "capacity") {
            self.groups.sort(function (left, right) {
                return left.capacity() - right.capacity();
            });
        }
        if (str == "maxcap") {
            self.groups.sort(function (left, right) {
                return left.maxcap() - right.maxcap();
            });
        }
     
    }

    self.sortStudents = function (str) {
        if (str == "name") {
            self.students.sort(function (left, right) {
                return left.name() == right.name() ? 0 : (left.name() < right.name() ? -1 : 1)
            });
        }
        if (str == "surname") {
            self.students.sort(function (left, right) {
                return left.surname() == right.surname() ? 0 : (left.surname() < right.surname() ? -1 : 1)
            });
        }
        if (str == "score") {
            self.students.sort(function (left, right) {
                return left.average_score() - right.average_score();
            });
        }
        if (str == "birthday") {
            self.students.sort(function (left, right) {
                var date1 = new Date(left.birthday());
                var date2 = new Date(right.birthday());
                return date1.getTime() - date2.getTime();
            });
        }
        if (str == "exp") {
            self.students.sort(function (left, right) {
                return left.exp() - right.exp();
            });
        }

    }


    self.newStudent = function () {
        // Post to server
        $.ajax("/student/create", {
            data: ko.toJSON({
                Name: self.newName,
                Surname: self.newSurname,
                Average_score: self.newAverage_score,
                Birthday: self.newBirthday,
                Exp: self.newExp
            }),
            type: "post", contentType: "application/json",
            success: function (result) {console.log("#student registered")}
        });
        // Clear form
        self.newName("");
        self.newSurname("");
        self.newAverage_score("");
        self.newBirthday("");
        self.newExp("");
    };


    $.getJSON("/group", function (allData) {
        var mappedGroups = $.map(allData, function (item) { return new Group(item) });
        self.groups(mappedGroups);
    });

   

    // Routes
    Sammy(function () {

        // All Groups
        this.get('#groups', function () {
            self.GroupId(null);
            self.GroupData(null);
            $.get("/group", {}, self.AllGroups);
        });

        // Students of the group
        this.get('#groups/students/:groupId', function () {
            self.GroupId(this.params.groupId);
            self.AllGroups(null);
            $.get("/group/students", { id: this.params.groupId }, self.GroupData);

            $.getJSON("/group/students", { id: this.params.groupId },function (GroupData) {
                var mappedStudents = $.map(GroupData, function (item) { return new Student(item) });
                self.students(mappedStudents);
            });

        });

        this.get('', function () { this.app.runRoute('get', '#groups') });
    }).run();

}

ko.applyBindings(new TestViewModel());