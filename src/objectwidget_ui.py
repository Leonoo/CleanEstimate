# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ui/objectwidget.ui'
#
# Created: Sat Dec 11 20:31:41 2010
#      by: PySide uic UI code generator
#
# WARNING! All changes made in this file will be lost!

from PySide import QtCore, QtGui

class Ui_ObjectWidget(object):
    def setupUi(self, ObjectWidget):
        ObjectWidget.setObjectName("ObjectWidget")
        ObjectWidget.resize(762, 558)
        self.horizontalLayout_8 = QtGui.QHBoxLayout(ObjectWidget)
        self.horizontalLayout_8.setObjectName("horizontalLayout_8")
        self.tableView = QtGui.QTableView(ObjectWidget)
        self.tableView.setMaximumSize(QtCore.QSize(300, 16777215))
        self.tableView.setObjectName("tableView")
        self.horizontalLayout_8.addWidget(self.tableView)
        self.verticalLayout_2 = QtGui.QVBoxLayout()
        self.verticalLayout_2.setSizeConstraint(QtGui.QLayout.SetMaximumSize)
        self.verticalLayout_2.setObjectName("verticalLayout_2")
        self.horizontalLayout = QtGui.QHBoxLayout()
        self.horizontalLayout.setContentsMargins(-1, -1, 0, -1)
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.label = QtGui.QLabel(ObjectWidget)
        self.label.setMinimumSize(QtCore.QSize(75, 0))
        self.label.setObjectName("label")
        self.horizontalLayout.addWidget(self.label)
        self.le_Name = QtGui.QLineEdit(ObjectWidget)
        self.le_Name.setMaximumSize(QtCore.QSize(200, 16777215))
        self.le_Name.setObjectName("le_Name")
        self.horizontalLayout.addWidget(self.le_Name)
        spacerItem = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout.addItem(spacerItem)
        self.verticalLayout_2.addLayout(self.horizontalLayout)
        self.horizontalLayout_2 = QtGui.QHBoxLayout()
        self.horizontalLayout_2.setObjectName("horizontalLayout_2")
        self.label_2 = QtGui.QLabel(ObjectWidget)
        self.label_2.setMinimumSize(QtCore.QSize(75, 0))
        self.label_2.setObjectName("label_2")
        self.horizontalLayout_2.addWidget(self.label_2)
        self.le_Street = QtGui.QLineEdit(ObjectWidget)
        self.le_Street.setMaximumSize(QtCore.QSize(200, 16777215))
        self.le_Street.setObjectName("le_Street")
        self.horizontalLayout_2.addWidget(self.le_Street)
        spacerItem1 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout_2.addItem(spacerItem1)
        self.verticalLayout_2.addLayout(self.horizontalLayout_2)
        self.horizontalLayout_6 = QtGui.QHBoxLayout()
        self.horizontalLayout_6.setObjectName("horizontalLayout_6")
        self.label_6 = QtGui.QLabel(ObjectWidget)
        self.label_6.setMinimumSize(QtCore.QSize(75, 0))
        self.label_6.setObjectName("label_6")
        self.horizontalLayout_6.addWidget(self.label_6)
        self.le_Number = QtGui.QLineEdit(ObjectWidget)
        self.le_Number.setMinimumSize(QtCore.QSize(0, 0))
        self.le_Number.setMaximumSize(QtCore.QSize(200, 16777215))
        self.le_Number.setObjectName("le_Number")
        self.horizontalLayout_6.addWidget(self.le_Number)
        spacerItem2 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout_6.addItem(spacerItem2)
        self.verticalLayout_2.addLayout(self.horizontalLayout_6)
        self.horizontalLayout_3 = QtGui.QHBoxLayout()
        self.horizontalLayout_3.setObjectName("horizontalLayout_3")
        self.label_3 = QtGui.QLabel(ObjectWidget)
        self.label_3.setMinimumSize(QtCore.QSize(75, 0))
        self.label_3.setObjectName("label_3")
        self.horizontalLayout_3.addWidget(self.label_3)
        self.le_Zip = QtGui.QLineEdit(ObjectWidget)
        self.le_Zip.setMinimumSize(QtCore.QSize(0, 0))
        self.le_Zip.setMaximumSize(QtCore.QSize(200, 16777215))
        self.le_Zip.setObjectName("le_Zip")
        self.horizontalLayout_3.addWidget(self.le_Zip)
        spacerItem3 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout_3.addItem(spacerItem3)
        self.verticalLayout_2.addLayout(self.horizontalLayout_3)
        self.horizontalLayout_4 = QtGui.QHBoxLayout()
        self.horizontalLayout_4.setObjectName("horizontalLayout_4")
        self.label_4 = QtGui.QLabel(ObjectWidget)
        self.label_4.setMinimumSize(QtCore.QSize(75, 0))
        self.label_4.setObjectName("label_4")
        self.horizontalLayout_4.addWidget(self.label_4)
        self.le_City = QtGui.QLineEdit(ObjectWidget)
        self.le_City.setMinimumSize(QtCore.QSize(0, 0))
        self.le_City.setMaximumSize(QtCore.QSize(200, 16777215))
        self.le_City.setObjectName("le_City")
        self.horizontalLayout_4.addWidget(self.le_City)
        spacerItem4 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout_4.addItem(spacerItem4)
        self.verticalLayout_2.addLayout(self.horizontalLayout_4)
        self.verticalLayout_3 = QtGui.QVBoxLayout()
        self.verticalLayout_3.setObjectName("verticalLayout_3")
        self.label_7 = QtGui.QLabel(ObjectWidget)
        self.label_7.setMinimumSize(QtCore.QSize(75, 0))
        self.label_7.setObjectName("label_7")
        self.verticalLayout_3.addWidget(self.label_7)
        self.te_Note = QtGui.QTextEdit(ObjectWidget)
        self.te_Note.setObjectName("te_Note")
        self.verticalLayout_3.addWidget(self.te_Note)
        self.verticalLayout = QtGui.QVBoxLayout()
        self.verticalLayout.setObjectName("verticalLayout")
        self.horizontalLayout_7 = QtGui.QHBoxLayout()
        self.horizontalLayout_7.setObjectName("horizontalLayout_7")
        self.pb_Apply = QtGui.QPushButton(ObjectWidget)
        sizePolicy = QtGui.QSizePolicy(QtGui.QSizePolicy.Maximum, QtGui.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.pb_Apply.sizePolicy().hasHeightForWidth())
        self.pb_Apply.setSizePolicy(sizePolicy)
        self.pb_Apply.setObjectName("pb_Apply")
        self.horizontalLayout_7.addWidget(self.pb_Apply)
        self.pb_Calculation = QtGui.QPushButton(ObjectWidget)
        self.pb_Calculation.setEnabled(False)
        sizePolicy = QtGui.QSizePolicy(QtGui.QSizePolicy.Maximum, QtGui.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.pb_Calculation.sizePolicy().hasHeightForWidth())
        self.pb_Calculation.setSizePolicy(sizePolicy)
        self.pb_Calculation.setObjectName("pb_Calculation")
        self.horizontalLayout_7.addWidget(self.pb_Calculation)
        spacerItem5 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout_7.addItem(spacerItem5)
        self.verticalLayout.addLayout(self.horizontalLayout_7)
        self.pb_New = QtGui.QPushButton(ObjectWidget)
        sizePolicy = QtGui.QSizePolicy(QtGui.QSizePolicy.Maximum, QtGui.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.pb_New.sizePolicy().hasHeightForWidth())
        self.pb_New.setSizePolicy(sizePolicy)
        self.pb_New.setObjectName("pb_New")
        self.verticalLayout.addWidget(self.pb_New)
        self.verticalLayout_3.addLayout(self.verticalLayout)
        self.verticalLayout_2.addLayout(self.verticalLayout_3)
        self.horizontalLayout_8.addLayout(self.verticalLayout_2)

        self.retranslateUi(ObjectWidget)
        QtCore.QMetaObject.connectSlotsByName(ObjectWidget)

    def retranslateUi(self, ObjectWidget):
        ObjectWidget.setWindowTitle(QtGui.QApplication.translate("ObjectWidget", "ObjectWidget", None, QtGui.QApplication.UnicodeUTF8))
        self.label.setText(QtGui.QApplication.translate("ObjectWidget", "Name:", None, QtGui.QApplication.UnicodeUTF8))
        self.label_2.setText(QtGui.QApplication.translate("ObjectWidget", "Street:", None, QtGui.QApplication.UnicodeUTF8))
        self.label_6.setText(QtGui.QApplication.translate("ObjectWidget", "Number:", None, QtGui.QApplication.UnicodeUTF8))
        self.label_3.setText(QtGui.QApplication.translate("ObjectWidget", "Postcode:", None, QtGui.QApplication.UnicodeUTF8))
        self.label_4.setText(QtGui.QApplication.translate("ObjectWidget", "City:", None, QtGui.QApplication.UnicodeUTF8))
        self.label_7.setText(QtGui.QApplication.translate("ObjectWidget", "Note:", None, QtGui.QApplication.UnicodeUTF8))
        self.pb_Apply.setText(QtGui.QApplication.translate("ObjectWidget", "Apply", None, QtGui.QApplication.UnicodeUTF8))
        self.pb_Calculation.setText(QtGui.QApplication.translate("ObjectWidget", "Calculation", None, QtGui.QApplication.UnicodeUTF8))
        self.pb_New.setText(QtGui.QApplication.translate("ObjectWidget", "New", None, QtGui.QApplication.UnicodeUTF8))


if __name__ == "__main__":
    import sys
    app = QtGui.QApplication(sys.argv)
    ObjectWidget = QtGui.QWidget()
    ui = Ui_ObjectWidget()
    ui.setupUi(ObjectWidget)
    ObjectWidget.show()
    sys.exit(app.exec_())
