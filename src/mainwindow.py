#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from mainwindow_ui import Ui_MainWindow
from companywidget import CompanyWidget
from objectwidget import ObjectWidget

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
        u"Etage varchar(255) NOT NULL,"
        u"BezeichnungID INTEGER NOT NULL,"
        u"BelaegeID INTEGER,"
        u"MethodeID INTEGER,"
        u"Einheit varchar(255),"
        u"Anzahl INTEGER,"
        u"Richtleistung INTEGER,"
        u"HaeufigkeitID INTEGER NOT NULL,"
        u"Flaeche double,"
        u"AusfuehrungszeitT double,"
        u"AusfuehrungszeitM double,"
        u"PreisM double NOT NULL,"
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
        customerID = self.__model.record(row).value(0)
        
        model = QtSql.QSqlRelationalTableModel() 
        model.setTable("Object")
        model.setEditStrategy(QtSql.QSqlRelationalTableModel.OnRowChange)
        fil = "KundenID = " + str(customerID)
        print(fil)
        model.setFilter(fil)
        
        self.__objectWidget = ObjectWidget(customerID, self)
        self.__ui.actionCompany.setVisible(True)
        self.setCentralWidget(self.__objectWidget)
        self.__objectWidget.setModel(model)
        self.__objectModel = model
        print(row)
        print(customerID)

    def actionCompany_triggered(self):
        self.__ui.actionCompany.setVisible(False)
        self.__objectWidget = None
        self.__objectModel = None
        
        self.showCompany()
    
if __name__ == "__main__":
    app = QtGui.QApplication(sys.argv)
    mainWindow = MainWindow()
    mainWindow.show()
    sys.exit(app.exec_())