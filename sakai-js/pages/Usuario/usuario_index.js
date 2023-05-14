import { Button } from 'primereact/button';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import React, { useState, useEffect, useRef } from 'react';
import { Toast } from 'primereact/toast';
import axios from 'axios'
import { classNames } from 'primereact/utils';
import { Dropdown } from 'primereact/dropdown';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';

const Usuarios = () => {
    const [posts, setPosts] = useState([]);
    const [searchText, setSearchText] = useState(''); //para la barra de busqueda
    const [submitted, setSubmitted] = useState(false);

    const [CreateModal, setCreateModal] = useState(false); // abrir el modal crear
    const [DeleteModal, setDeleteModal] = useState(false); //abrir el modal eliminar
    const [EditModal, setEditModal] = useState(false);//abrir el modal editar

    const toast = useRef(null); //para los toast

    const [UsuarioId, setUsuarioId] = useState(''); // id del usuario
    const [NombreUsuario, setNombreUsuario] = useState(''); //nombre de usuario
    const [Contrasenia, setContrasenia] = useState(''); //Contraseña
    const [EsAdmin, setEsAdmin] = useState(false); //Es Admin?

    const [RolesDDL, setRolesDDL] = useState([]); //ddl roles
    const [Rol, setRol] = useState(''); //almacena el valor seleccionado del ddl 

    const [EmpleadoDDL, setEmpleadoDDL] = useState([]); //ddl Empleados
    const [Empleado, setEmpleado] = useState(''); //almacena el valor seleccionado del ddl 


    const [loading, setLoading] = useState(true);
    useEffect(() => {
        const url = Global.url + 'Usuario/Listado';

        if (loading) {
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    setPosts(data.data);
                    setLoading(false);
                });
        }
    }, [loading]);

    //cargar ddl 
    useEffect(() => {

        axios.get(Global.url + 'Rol/Listado')
            .then(response => response.data)
            .then((data) => setRolesDDL(data.data.map((c) => ({ code: c.role_Id, name: c.role_Nombre }))))
            .catch(error => console.error(error))

        axios.get(Global.url + 'Empleado/Listado')
            .then(response => response.data)
            .then((data) => setEmpleadoDDL(data.data.map((c) => ({ code: c.empe_Id, name: c.empe_NombreCompleto }))))
            .catch(error => console.error(error))

    }, []);


    //cerrar modal crear
    const hideDialog = () => {
        setSubmitted(false);
        setNombreUsuario('');
        setContrasenia('');
        setEmpleado('');
        setRol('');
        setEsAdmin(false);
        setCreateModal(false);
    };

    //mandar datos de ingresar a la api
    const IngresarUsuario = (e) => {

        if (!NombreUsuario || !Contrasenia || !Rol || !Empleado) {

            setSubmitted(true);
        }
        else {
            let payload = {
                user_NombreUsuario: NombreUsuario,
                user_Contrasena: Contrasenia,
                user_EsAdmin: EsAdmin,
                role_Id: Rol.code,
                empe_Id: Empleado.code,
                user_UsuCreacion: 1
            }
            axios.post(Global.url + 'Usuario/Insertar', payload)
                .then((r) => {
                    hideDialog();
                    setLoading(true);
                    toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Ingresado Correctamente', life: 2000 });
                })
                .catch((e) => {
                    toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
                })
        }
    }


    /* MODAL ELIMINAR */
    //abrir modal eliminar
    const OpenDeleteModal = (id) => {
        setUsuarioId(id);
        setDeleteModal(true);
    }

    //cerrar modal eliminar
    const hideDeleteModal = () => {
        setUsuarioId("");
        setDeleteModal(false);
    };

    //mandar datos del eliminar a la api
    const EliminarUsuarios = (e) => {

        let payloadDelete = {
            user_Id: parseInt(UsuarioId),
        }
        axios.post(Global.url + 'Usuario/Eliminar', payloadDelete)
            .then((r) => {
                setLoading(true);
                hideDeleteModal();
                setUsuarioId("");
                toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Eliminado Correctamente', life: 1500 });
            })
            .catch((e) => {
                toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
            })
    }

    /* MODAL EDITAR */
    //traer los datos para el editar
    const LlamarDatosEdit = (id) => {
        axios.get(Global.url + 'Usuario/Buscar?id=' + id)
            .then((r) => {
                return r.data; // La respuesta ya está en formato JSON
            })
            .then((data) => {

                setEsAdmin(data.user_EsAdmin)

                var codeRol = { code: data.role_Id, name: data.role_Nombre }
                setRol(codeRol);

                var codeEmpleado = { code: data.empe_Id, name: data.empe_NombreCompleto }
                setEmpleado(codeEmpleado);

                setUsuarioId(id);
                setEditModal(true)

            })
            .catch(error => console.error(error))
    }


    //cerrar modal Editar
    const hideEditModal = () => {
        setEditModal('');
        setRol('');
        setEsAdmin(false);
        setSubmitted(false);
        setEditModal(false);
    };


    //Mandar la categoria ya editada a la api
    const DatosEditados = (e) => {

        if (!Rol || !Empleado) {

            setSubmitted(true);

        }
        else {

            let payloadEdit = {
                user_Id: parseInt(UsuarioId),
                user_NombreUsuario: NombreUsuario,
                user_Contrasena: Contrasenia,
                user_EsAdmin: EsAdmin,
                role_Id: Rol.code,
                empe_Id: Empleado.code,
                user_UsuCreacion: 1
            }
            console.log(payloadEdit)
            axios.post(Global.url + 'Usuario/Editar', payloadEdit)
                .then((r) => {
                    hideEditModal();
                    setLoading(true);
                    toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Editado Correctamente', life: 1500 });
                })
                .catch((e) => {
                    toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
                })
        }
    }



    const header = (
        <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
            <div className="grid">
                <div className="col-12">
                    <Button type="button" label="Nuevo" severity="success" outlined icon="pi pi-upload" onClick={() => setCreateModal(true)} />
                </div>
            </div>
            <span className="block mt-2 md:mt-0 p-input-icon-left">
                <i className="pi pi-search" />
                <InputText type="text" value={searchText} onChange={e => setSearchText(e.target.value)} placeholder="Buscar..." />
            </span>
        </div>
    );

    return (
        <div className="grid">
            <div className="col-12">

                <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                    <div className="row text-center d-flex align-items-center">
                        <h2 style={{ color: 'white' }}>Usuarios</h2>
                    </div>
                </div>


                <div className="card">
                    <Toast ref={toast} />
                    <DataTable
                        paginator
                        className="p-datatable-gridlines"
                        showGridlines
                        sortField="representative.name"
                        rows={10}
                        rowsPerPageOptions={[5, 10, 25]}
                        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                        currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} registros."
                        dataKey="user_Id"
                        filterDisplay="menu"
                        responsiveLayout="scroll"
                        emptyMessage="No se encontrron registros."
                        filterMode="filter"
                        header={header}
                        value={posts.filter((post) =>
                            post.user_NombreUsuario.toLowerCase().includes(searchText.toLowerCase()) ||
                            post.empe_NombreCompleto.toLowerCase().includes(searchText.toLowerCase()) ||
                            post.role_Nombre.toLowerCase().includes(searchText.toLowerCase())

                        )}

                    >
                        <Column field="user_NombreUsuario" header="Nombre Usuario" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                        <Column field="empe_NombreCompleto" header="Emmpleado" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                        <Column field="role_Nombre" header="Rol" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />

                        <Column
                            field="acciones"
                            header="Acciones"
                            headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}
                            style={{ minWidth: '300px' }}
                            body={rowData => (
                                <div>
                                    <Button label="Detalles" severity="info" icon="pi pi-eye" outlined style={{ fontSize: '0.8rem' }} /> .
                                    <Button label="Editar" severity="warning" icon="pi pi-upload" outlined style={{ fontSize: '0.8rem' }} onClick={() => LlamarDatosEdit(rowData.user_Id)} /> .
                                    <Button label="Eliminar" severity="danger" icon="pi pi-trash" outlined style={{ fontSize: '0.8rem' }} onClick={() => OpenDeleteModal(rowData.user_Id)} />
                                </div>
                            )}
                        />
                    </DataTable>

                    {/*modal para Ingresar datos*/}
                    <Dialog
                        visible={CreateModal}
                        style={{ width: '500px' }}
                        header="Ingresar Usuarios"
                        modal
                        className="p-fluid"
                        onHide={hideDialog}
                        footer={
                            <div>
                                <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDialog} />
                                <Button label="Guardar" icon="pi pi-check" severity="success" onClick={IngresarUsuario} />
                            </div>
                        }
                    >
                        <div className="col-12">
                            <div className="grid p-fluid">
                                <div className="col-6">
                                    <div className="field">
                                        <label htmlFor="inputtext">Nombre Usuario</label><br />
                                        <InputText type="text" id="inputtext" value={NombreUsuario} onChange={(e) => setNombreUsuario(e.target.value)} className={classNames({ 'p-invalid': submitted && !NombreUsuario })} />
                                        {submitted && !NombreUsuario && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                                    </div>
                                </div>

                                <div className="col-6">
                                    <div className="field">
                                        <label htmlFor="inputtext">Contraseña</label><br />
                                        <InputText type="text" id="inputtext" value={Contrasenia} onChange={(e) => setContrasenia(e.target.value)} className={classNames({ 'p-invalid': submitted && !Contrasenia })} />
                                        {submitted && !Contrasenia && <small className="p-invalid" style={{ color: 'red' }}>El campo es requerido.</small>}
                                    </div>
                                </div>

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Sexo">Rol</label><br />
                                        <Dropdown optionLabel="name" placeholder="Seleccionar" options={RolesDDL} value={Rol} onChange={(e) => setRol(e.value)} className={classNames({ 'p-invalid': submitted && !Rol })} />
                                        {submitted && !Rol && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                    </div>
                                </div>

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Empleado">Empleado</label><br />
                                        <Dropdown optionLabel="name" placeholder="Selecionar" options={EmpleadoDDL} value={Empleado} onChange={(e) => setEmpleado(e.value)} className={classNames({ 'p-invalid': submitted && !Empleado })} />
                                        {submitted && !Empleado && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                    </div>
                                </div>

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Sexo">Es Admin</label><br />
                                        <InputSwitch checked={EsAdmin} onChange={(e) => setEsAdmin(e.value)} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Dialog>

                    {/*modal para editar datos*/}
                    <Dialog
                        visible={EditModal}
                        style={{ width: '500px' }}
                        header="Ingresar Sucursal"
                        modal
                        className="p-fluid"
                        onHide={hideEditModal}
                        footer={
                            <div>
                                <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideEditModal} />
                                <Button label="Guardar" icon="pi pi-check" severity="success" onClick={DatosEditados} />
                            </div>
                        }
                    >
                        <div className="col-12">
                            <div className="grid p-fluid">

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Sexo">Rol</label><br />
                                        <Dropdown optionLabel="name" placeholder="Seleccionar" options={RolesDDL} value={Rol} onChange={(e) => setRol(e.value)} className={classNames({ 'p-invalid': submitted && !Rol })} />
                                        {submitted && !Rol && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                    </div>
                                </div>

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Empleado">Empleado</label><br />
                                        <Dropdown optionLabel="name" placeholder="Selecionar" options={EmpleadoDDL} value={Empleado} onChange={(e) => setEmpleado(e.value)} className={classNames({ 'p-invalid': submitted && !Empleado })} />
                                        {submitted && !Empleado && <small className="p-invalid" style={{ color: 'red' }}>Seleccione una opcion.</small>}
                                    </div>
                                </div>

                                <div className='col-6'>
                                    <div className="field">
                                        <label htmlFor="Sexo">Es Admin</label><br />
                                        <InputSwitch checked={EsAdmin} onChange={(e) => setEsAdmin(e.value)} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Dialog>


                    {/*modal para Eliminar Registros*/}
                    <Dialog visible={DeleteModal} style={{ width: '450px' }} header="Eliminar Usuarios" onHide={hideDeleteModal} modal footer={
                        <>
                            <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDeleteModal} />
                            <Button label="Confirmar" icon="pi pi-check" severity="success" onClick={EliminarUsuarios} />
                        </>
                    }>
                        <div className="flex align-items-center justify-content-center">
                            <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                            <span>
                                ¿Desea eliminar este registro?
                            </span>
                        </div>
                    </Dialog>

                </div>
            </div>
        </div>
    )



}

export default Usuarios;