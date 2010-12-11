#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from companywidget_ui import Ui_CompanyWidget

class CompanyWidget(QtGui.QWidget):
    gotoObject = QtCore.Signal(int)
    speak = QtCore.Signal((int,), (str,))
    
    def __init__(self, parent=None):
        super(CompanyWidget, self).__init__(parent)

        #Private
        self.__NewMode = False
        self.__ui = Ui_CompanyWidget()
        self.__model = None
        self.__mapper = None

        self.__ui.setupUi(self)

        self.__ui.pb_Apply.clicked.connect(self.pb_Apply_clicked)
        self.__ui.pb_New.clicked.connect(self.pb_New_clicked)
        self.__ui.pb_Objects.clicked.connect(self.pb_Objects_clicked)
        self.__ui.le_ID.textChanged.connect(self.le_ID_textChanged)

        self.__mapper = QtGui.QDataWidgetMapper(self)
        self.__mapper.setSubmitPolicy(QtGui.QDataWidgetMapper.ManualSubmit)
        self.__mapper.setItemDelegate(QtSql.QSqlRelationalDelegate(self.__mapper))

    def setModel(self, model):
        self.__model = model
        self.__ui.tableView.setModel(model)
        self.__ui.tableView.setItemDelegate(QtSql.QSqlRelationalDelegate( self.__ui.tableView))
        
        self.__ui.tableView.setEditTriggers(QtGui.QAbstractItemView.NoEditTriggers);
        self.__ui.tableView.setSelectionMode(QtGui.QAbstractItemView.SingleSelection);
        self.__ui.tableView.setSelectionBehavior(QtGui.QAbstractItemView.SelectRows);

        self.__ui.tableView.setSortingEnabled(True)
        self.__ui.tableView.sortByColumn(0,QtCore.Qt.AscendingOrder)
        
        self.__ui.tableView.clicked.connect(self.clicked)

        self.__ui.tableView.setColumnHidden(2, True)
        self.__ui.tableView.setColumnHidden(3, True)
        self.__ui.tableView.setColumnHidden(4, True)
        self.__ui.tableView.setColumnHidden(5, True)
        self.__ui.tableView.setColumnHidden(6, True)

        self.__ui.tableView.horizontalHeader().setResizeMode(0, QtGui.QHeaderView.Fixed)
        self.__ui.tableView.horizontalHeader().setResizeMode(1, QtGui.QHeaderView.Stretch)
        self.__ui.tableView.horizontalHeader().setDefaultAlignment(QtCore.Qt.AlignLeft)
        self.__ui.tableView.setColumnWidth(0, 80)

        self.__mapper.setModel(model)
        self.__mapper.addMapping(self.__ui.le_ID, 0)
        self.__mapper.addMapping(self.__ui.le_Name, 1)
        self.__mapper.addMapping(self.__ui.le_Street, 2)
        self.__mapper.addMapping(self.__ui.le_Number, 3)
        self.__mapper.addMapping(self.__ui.le_Zip, 4)
        self.__mapper.addMapping(self.__ui.le_City, 5)
        self.__mapper.addMapping(self.__ui.te_Note, 6)        

    def getModel(self):
        return self.__model

    def pb_Apply_clicked(self):
        if len(self.__ui.le_ID.text().strip()) > 0:
            if len(self.__ui.le_Name.text().strip()) > 0:
                self.__ui.le_Name.setText(self.__ui.le_Name.text().strip(" "))
                if self.__mapper.submit():
                    self.__mapper.setCurrentIndex(self.newIndex(int(self.__ui.le_ID.text())))
                    if self.__NewMode:
                        self.__NewMode = False
                        self.__ui.pb_Objects.setDisabled(False)

                if self.__model.lastError().number() != -1:
                    print(self.__model.lastError())
                    QtGui.QMessageBox.critical(self, self.trUtf8("Fehler!!!"),
                        self.trUtf8("Fehler beim einfügen des Kunden."),
                        QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)
                    print("Error")
            else:
                QtGui.QMessageBox.critical(self, self.trUtf8("Fehler!!!"),
                self.trUtf8("Bitte einen Namen angeben."),
                QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)
        else:
            QtGui.QMessageBox.critical(self, self.trUtf8("Fehler!!!"),
                self.trUtf8("Bitte Kundennummer angeben."),
                QtGui.QMessageBox.Cancel, QtGui.QMessageBox.NoButton)            

    def pb_New_clicked(self):
        if not self.__NewMode:
            self.__NewMode = True
            self.__ui.pb_Objects.setDisabled(True)
            self.__ui.le_ID.clear()

            self.__record = self.__model.record()
            self.__model.insertRecord(-1, self.__record)
            self.__mapper.setCurrentIndex(self.__model.rowCount() - 1)
            self.__ui.tableView.selectRow(self.__model.rowCount() - 1)

    def pb_Objects_clicked(self):
        print(self.__mapper.currentIndex())
        self.gotoObject.emit(self.__mapper.currentIndex())

    def clicked(self, index):
        print(index)
        if self.__NewMode:
            self.__NewMode = False
            print("revert")
            self.__model.revertAll()
            
        self.__ui.le_ID.clear()
        self.__ui.pb_Objects.setDisabled(False)
        self.__mapper.setCurrentIndex(index.row())

    def le_ID_textChanged(self, newstring):
        print(newstring)

    def newIndex(self, id):
        rowCount = self.__model.rowCount()
        for i in xrange(rowCount):
            if self.__model.data(self.__model.index(i,0)) == id:
                return i

        return -1