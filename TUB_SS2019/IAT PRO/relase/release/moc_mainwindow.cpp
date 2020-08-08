/****************************************************************************
** Meta object code from reading C++ file 'mainwindow.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.9.6)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../GesamtV01/mainwindow.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'mainwindow.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.9.6. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_MainWindow_t {
    QByteArrayData data[22];
    char stringdata0[348];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_MainWindow_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_MainWindow_t qt_meta_stringdata_MainWindow = {
    {
QT_MOC_LITERAL(0, 0, 10), // "MainWindow"
QT_MOC_LITERAL(1, 11, 17), // "on_Browse_clicked"
QT_MOC_LITERAL(2, 29, 0), // ""
QT_MOC_LITERAL(3, 30, 20), // "on_LED1_stateChanged"
QT_MOC_LITERAL(4, 51, 4), // "arg1"
QT_MOC_LITERAL(5, 56, 20), // "on_LED2_stateChanged"
QT_MOC_LITERAL(6, 77, 20), // "on_LED3_stateChanged"
QT_MOC_LITERAL(7, 98, 20), // "on_LED4_stateChanged"
QT_MOC_LITERAL(8, 119, 9), // "updateLED"
QT_MOC_LITERAL(9, 129, 10), // "DriveMotor"
QT_MOC_LITERAL(10, 140, 5), // "Speed"
QT_MOC_LITERAL(11, 146, 6), // "Degree"
QT_MOC_LITERAL(12, 153, 23), // "on_Browse_Photo_clicked"
QT_MOC_LITERAL(13, 177, 23), // "on_Browse_Model_clicked"
QT_MOC_LITERAL(14, 201, 24), // "on_OpenCameraBtn_clicked"
QT_MOC_LITERAL(15, 226, 21), // "on_TakeAPhoto_clicked"
QT_MOC_LITERAL(16, 248, 21), // "on_TakePhotos_clicked"
QT_MOC_LITERAL(17, 270, 20), // "on_MotorMove_clicked"
QT_MOC_LITERAL(18, 291, 25), // "on_CloseCameraBtn_clicked"
QT_MOC_LITERAL(19, 317, 8), // "getFrame"
QT_MOC_LITERAL(20, 326, 9), // "takePhoto"
QT_MOC_LITERAL(21, 336, 11) // "openProcess"

    },
    "MainWindow\0on_Browse_clicked\0\0"
    "on_LED1_stateChanged\0arg1\0"
    "on_LED2_stateChanged\0on_LED3_stateChanged\0"
    "on_LED4_stateChanged\0updateLED\0"
    "DriveMotor\0Speed\0Degree\0on_Browse_Photo_clicked\0"
    "on_Browse_Model_clicked\0"
    "on_OpenCameraBtn_clicked\0on_TakeAPhoto_clicked\0"
    "on_TakePhotos_clicked\0on_MotorMove_clicked\0"
    "on_CloseCameraBtn_clicked\0getFrame\0"
    "takePhoto\0openProcess"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_MainWindow[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
      17,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    0,   99,    2, 0x08 /* Private */,
       3,    1,  100,    2, 0x08 /* Private */,
       5,    1,  103,    2, 0x08 /* Private */,
       6,    1,  106,    2, 0x08 /* Private */,
       7,    1,  109,    2, 0x08 /* Private */,
       8,    1,  112,    2, 0x08 /* Private */,
       9,    2,  115,    2, 0x08 /* Private */,
      12,    0,  120,    2, 0x08 /* Private */,
      13,    0,  121,    2, 0x08 /* Private */,
      14,    0,  122,    2, 0x08 /* Private */,
      15,    0,  123,    2, 0x08 /* Private */,
      16,    0,  124,    2, 0x08 /* Private */,
      17,    0,  125,    2, 0x08 /* Private */,
      18,    0,  126,    2, 0x08 /* Private */,
      19,    0,  127,    2, 0x08 /* Private */,
      20,    0,  128,    2, 0x08 /* Private */,
      21,    0,  129,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void,
    QMetaType::Void, QMetaType::Int,    4,
    QMetaType::Void, QMetaType::Int,    4,
    QMetaType::Void, QMetaType::Int,    4,
    QMetaType::Void, QMetaType::Int,    4,
    QMetaType::Void, QMetaType::QString,    2,
    QMetaType::Void, QMetaType::Int, QMetaType::Int,   10,   11,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,

       0        // eod
};

void MainWindow::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        MainWindow *_t = static_cast<MainWindow *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->on_Browse_clicked(); break;
        case 1: _t->on_LED1_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 2: _t->on_LED2_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 3: _t->on_LED3_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 4: _t->on_LED4_stateChanged((*reinterpret_cast< int(*)>(_a[1]))); break;
        case 5: _t->updateLED((*reinterpret_cast< QString(*)>(_a[1]))); break;
        case 6: _t->DriveMotor((*reinterpret_cast< int(*)>(_a[1])),(*reinterpret_cast< int(*)>(_a[2]))); break;
        case 7: _t->on_Browse_Photo_clicked(); break;
        case 8: _t->on_Browse_Model_clicked(); break;
        case 9: _t->on_OpenCameraBtn_clicked(); break;
        case 10: _t->on_TakeAPhoto_clicked(); break;
        case 11: _t->on_TakePhotos_clicked(); break;
        case 12: _t->on_MotorMove_clicked(); break;
        case 13: _t->on_CloseCameraBtn_clicked(); break;
        case 14: _t->getFrame(); break;
        case 15: _t->takePhoto(); break;
        case 16: _t->openProcess(); break;
        default: ;
        }
    }
}

const QMetaObject MainWindow::staticMetaObject = {
    { &QMainWindow::staticMetaObject, qt_meta_stringdata_MainWindow.data,
      qt_meta_data_MainWindow,  qt_static_metacall, nullptr, nullptr}
};


const QMetaObject *MainWindow::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *MainWindow::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_MainWindow.stringdata0))
        return static_cast<void*>(this);
    return QMainWindow::qt_metacast(_clname);
}

int MainWindow::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QMainWindow::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 17)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 17;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 17)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 17;
    }
    return _id;
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
