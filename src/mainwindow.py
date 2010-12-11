#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from mainwindow_ui import Ui_MainWindow

from companywidget import CompanyWidget
from objectwidget import ObjectWidget
from calculationwidget import CalculationWidget
from addwidget import AddWidget

class MainWindow(QtGui.QMainWindow):
    def __init__(self, parent=None):
        super(MainWindow, self).__init__(parent)

        #Private
        self.__ui = Ui_MainWindow()
        self.__model = None
        self.__objectModel = None
        self.__newWidget = None
        self.__objectWidget = None
        
        self.__ui.setupUi(self)
        self.createConnection()
        self.createModel()

        self.__ui.actionCompany.triggered.connect(self.actionCompany_triggered)      

        self.__ui.actionIdentifiers.triggered.connect(self.actionIdentifiers_triggered)
        self.__ui.actionCoatings.triggered.connect(self.actionCoatings_triggered)
        

        self.showCompany()

    def createConnection(self):
        db = QtSql.QSqlDatabase.addDatabase("QSQLITE")
        db.setDatabaseName("./test.db3")

        if not db.open():
            QtGui.QMessageBox.critical(None, QtGui.qApp.tr("Cannot open database"),
            QtGui.qApp.tr("Unable to establish a database connection.\n"
            "This example needs SQLite support. Please read "
            "the Qt SQL driver documentation for information "
            "how to build it.\n\nClick Cancel to exit."),
            QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)
            return False

        query = QtSql.QSqlQuery()
        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Kunde(Id INTEGER primary key,"
        u"Name varchar(255) NOT NULL,"
        u"Straße varchar(255),"
        u"Nummer varchar(255),"
        u"Plz varchar(255),"
        u"Ort varchar(255),"
        u"Beschreibung TEXT"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Object(Id INTEGER primary key AUTOINCREMENT,"
        u"KundenID INTEGER NOT NULL,"
        u"Name varchar(255) NOT NULL,"
        u"Straße varchar(255),"
        u"Nummer varchar(255),"
        u"Plz varchar(255),"
        u"Ort varchar(255),"
        u"Beschreibung TEXT,"
        u"SVS DOUBLE,"
        u"Arbeitstage INTEGER,"
        u"Datum DateTime"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Kalkulation(Id INTEGER primary key AUTOINCREMENT,"
        u"ObjektID INTEGER NOT NULL,"
        u"Etage varchar(255),"
        u"BezeichnungID INTEGER,"
        u"BelaegeID INTEGER,"
        u"MethodeID INTEGER,"
        u"Einheit varchar(255),"
        u"Anzahl INTEGER,"
        u"Richtleistung INTEGER,"
        u"HaeufigkeitID INTEGER,"
        u"Flaeche double,"
        u"AusfuehrungszeitT double,"
        u"AusfuehrungszeitM double,"
        u"PreisM double,"
        u"Mietpreis double"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Bezeichnung(Id INTEGER primary key AUTOINCREMENT,"
        u"Name varchar(255) NOT NULL"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Belaege(Id INTEGER primary key AUTOINCREMENT,"
        u"Name varchar(255) NOT NULL"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Methode(Id INTEGER primary key AUTOINCREMENT,"
        u"Name varchar(255) NOT NULL"
        u")"
        u";")
        print (ok)

        ok = query.exec_(u"CREATE TABLE IF NOT EXISTS Haeufigkeit(Id INTEGER primary key AUTOINCREMENT,"
        u"Name varchar(255) NOT NULL,"
        u"Haeufigfaktor double NOT NULL,"
        u"Beschreibung varchar(255)"
        u")"
        u";")
        print (ok)

        return True

    def createModel(self):
        model = QtSql.QSqlRelationalTableModel()

        model.setTable("Kunde")
        #model.setSort(0,QtCore.Qt.AscendingOrder)
        model.setHeaderData(0, QtCore.Qt.Horizontal, u"Customer ID")
        model.setHeaderData(1, QtCore.Qt.Horizontal, u"Name")
        #model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnManualSubmit)
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        model.select()
        self.__model = model

    def showCompany(self):
        self.__newWidget = CompanyWidget(self)
        self.__newWidget.setModel(self.__model)
        self.__newWidget.gotoObject.connect(self.showObject)

        self.setCentralWidget(self.__newWidget)

    def showObject(self, row):
        record = self.__model.record(row)
        customerID = record.value(0)
        
        model = QtSql.QSqlRelationalTableModel() 
        model.setTable("Object")
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        fil = "KundenID = " + str(customerID)
        print(fil)
        model.setFilter(fil)

        model.select()
        
        self.__objectWidget = ObjectWidget(customerID, self)
        self.__objectWidget.gotoCalculation.connect(self.showCalculation)
        self.__ui.actionCompany.setVisible(True)
        self.setCentralWidget(self.__objectWidget)
        self.__objectWidget.setModel(model)
        self.__objectModel = model
        print(row)
        print(customerID)

    def showCalculation(self, row):
        record = self.__objectModel.record(row)
        objectID = record.value(0)

        model = QtSql.QSqlRelationalTableModel()
        model.setTable("Kalkulation")
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        fil = "ObjektID = " + str(objectID)
        print(fil)
        model.setFilter(fil)

        relBezeichnung = QtSql.QSqlRelation(u"Bezeichnung", u"Id", u"Name")
        relBelaege = QtSql.QSqlRelation(u"Belaege", u"Id", u"Name")
        relMethode = QtSql.QSqlRelation(u"Methode", u"Id", u"Name")
        relHaeufigkeit = QtSql.QSqlRelation(u"Haeufigkeit", u"Id", u"Name")

        model.setRelation(model.fieldIndex(u"BezeichnungID"), relBezeichnung);
        model.setRelation(model.fieldIndex(u"BelaegeID"), relBelaege);
        model.setRelation(model.fieldIndex(u"MethodeID"), relMethode);
        model.setRelation(model.fieldIndex(u"HaeufigkeitID"), relHaeufigkeit);

        model.select()

        self.__calculationWidget = CalculationWidget(objectID)
        self.__ui.actionObject.setVisible(True)
        self.setCentralWidget(self.__calculationWidget)
        self.__calculationWidget.setModel(model)

        self.__calculationWidget.HourlyRate = record.value(8)
        self.__calculationWidget.Workdays = record.value(9)
        
        self.__calculationModel = model

    def actionCompany_triggered(self):
        self.__ui.actionCompany.setVisible(False)
        self.__objectWidget = None
        self.__objectModel = None
        
        self.showCompany()

    def actionIdentifiers_triggered(self):
        model = QtSql.QSqlTableModel()
        model.setTable(u"Bezeichnung")
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        model.setHeaderData(1, QtCore.Qt.Horizontal, self.tr("Identifiers"))
        
        model.select()
        
        addWidget = AddWidget("Identifiers", self)
        addWidget.setModel(model)
        addWidget.exec_()
        
    def actionCoatings_triggered(self):
        model = QtSql.QSqlTableModel()
        model.setTable(u"Belaege")
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        model.setHeaderData(1, QtCore.Qt.Horizontal, self.tr("Coatings"))

        model.select()

        addWidget = AddWidget("Coatings", self)
        addWidget.setModel(model)
        addWidget.exec_()
    
if __name__ == "__main__":
    app = QtGui.QApplication(sys.argv)
    mainWindow = MainWindow()
    mainWindow.show()
    sys.exit(app.exec_())