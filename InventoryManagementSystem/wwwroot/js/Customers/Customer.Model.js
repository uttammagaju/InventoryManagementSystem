/// <reference path="../knockout.js" />

var CustomerModel = function (item) {
    var self = item;
    item = item || {};
    self.FullName = ko.observable(item.fullName || '');
    self.ContactNo = ko.observable(item.contactNo || '');
    self.Address = ko.observable(item.address || '');
}