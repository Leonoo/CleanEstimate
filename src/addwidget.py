#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import sys
from PySide import QtCore, QtGui, QtSql

from addwidget_ui import Ui_AddWidget

class AddWidget(QtGui.QDialog):   
    def __init__(self, identi, parent=None):
        super(AddWidget, self).__init__(parent)

        #Private
        self.__ui = Ui_AddWidget()
        self.__model = None
        self.__identi = self.tr(identi)

        self.__ui.setupUi(self)

        self.__ui.pb_Add.clicked.connect(self.pb_Add_clicked)

    def setModel(self, model):
        self.__model = model
        self.__ui.tableView.setModel(model)
        self.__ui.tableView.setItemDelegate(QtSql.QSqlRelationalDelegate( self.__ui.tableView))
      
        self.__ui.tableView.setEditTriggers(QtGui.QAbstractItemView.NoEditTriggers);
        self.__ui.tableView.setSelectionMode(QtGui.QAbstractItemView.SingleSelection);
        self.__ui.tableView.setSelectionBehavior(QtGui.QAbstractItemView.SelectRows);

        self.__ui.tableView.setSortingEnabled(True)
        self.__ui.tableView.sortByColumn(1 ,QtCore.Qt.AscendingOrder)
        
        self.__ui.tableView.setColumnHidden(0, True)

        self.__ui.tableView.horizontalHeader().setResizeMode(1, QtGui.QHeaderView.Stretch)
        self.__ui.tableView.horizontalHeader().setDefaultAlignment(QtCore.Qt.AlignLeft)

    #def getModel(self):
    #    return self.__model

    #    def clicked(self, index):
    #    print(index)
    #    if self.__NewMode:
    #        self.__NewMode = False
    #        print("revert")
    #        self.__model.revertAll()

    #self.__ui.le_ID.clear()
    #    self.__ui.pb_Objects.setDisabled(False)
    #    self.__mapper.setCurrentIndex(index.row())

    def pb_Add_clicked(self):
        text, ok = QtGui.QInputDialog.getText(self, self.trUtf8(str(self.__identi + " input")),
                self.trUtf8(str(self.__identi)), QtGui.QLineEdit.Normal,
                self.trUtf8("None"))

        if ok and text != '':
            if self.identifiersUnique(text):
                record = self.__model.record()
                record.setValue(1, text)
                self.__model.insertRecord(-1, record)
                self.__model.submit()
            else:
                print("ERROR")

    def identifiersUnique(self, text):
        rowCount = self.__model.rowCount()
        for i in xrange(rowCount):
            if self.__model.data(self.__model.index(i, 1)) == text:
                return False
        return True