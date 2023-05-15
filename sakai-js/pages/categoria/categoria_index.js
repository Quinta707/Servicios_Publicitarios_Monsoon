import { Button } from 'primereact/button';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { InputText } from 'primereact/inputtext';
import { Dialog } from 'primereact/dialog';
import React, { useState, useEffect, useRef, use } from 'react';
import { Toast } from 'primereact/toast';
import axios from 'axios'
import { useRouter } from 'next/router';


const App = () => {

    const router = useRouter();
    const [posts, setPosts] = useState([]);
    const [searchText, setSearchText] = useState(''); //para la barra de busqueda

    const [productDialog, setProductDialog] = useState(false); // abrir el modal crear
    const [DeleteModal, setDeleteModal] = useState(false); //abrir el modal eliminar
    const [EditModal, setEditModal] = useState(false);//abrir el modal editar

    const [submitted, setSubmitted] = useState(false);

    const toast = useRef(null); //para los toast

    const [error, setError] = useState('');

    const [Desripcion, setDescripcion] = useState("");
    const [CategoriaId, setCategoriaId] = useState("");

    const [loading, setLoading] = useState(true)
    useEffect(() => {

        var admin = 0;
        var pant_Id = 3;
        var role_Id = 0;
       
        if (localStorage.getItem('role_Id') != null) {
            role_Id = localStorage.getItem('role_Id');
        }

        if (localStorage.getItem('user_EsAdmin') == 'true') {
            admin = 1;
        }

        axios.put(Global.url + `Pantalla/AccesoPantalla?esAdmin=${admin}&role_Id=${role_Id}&pant_Id=${pant_Id}`)
            .then((r) => {

                if (r.data[0][""] == 1) {
                    const url = Global.url + 'Categoria/Listado';
                    if (loading) {
                        fetch(url)
                            .then(response => response.json())
                            .then(data => {
                                setPosts(data.data)
                                setLoading(false);
                            })
                            .catch((e) =>{
                                toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
                              })
                    }
                }
                else {
                    router.push('/');
                }
            })

    }, [loading]);



    const header = (
        <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
            <div className="grid">
                <div className="col-12">
                    <Button type="button" label="Nuevo" severity="success" outlined icon="pi pi-upload" onClick={() => setProductDialog(true)} />
                </div>
            </div>
            <span className="block mt-2 md:mt-0 p-input-icon-left">
                <i className="pi pi-search" />
                <InputText type="text" value={searchText} onChange={e => setSearchText(e.target.value)} placeholder="Buscar..." />
            </span>
        </div>
    );

    /* MODAL CREAR */
    //validar campos vacios modal crear
    const saveProduct = () => {
        if (!Desripcion) {
            setError('Este campo no necesario.');
            return;
        }
        else {
            IngresarCategorias();
        }
    };

    //cerrar modal crear
    const hideDialog = () => {
        setSubmitted(false);
        setProductDialog(false);
        setError('');
        setDescripcion('');
    };

    //mandar datos de ingresar a la api
    const IngresarCategorias = (e) => {

        let payload = {
            cate_Descripcion: Desripcion,
            cate_UsuCreacion: 1,
        }
        axios.post(Global.url + 'Categoria/Insertar', payload)
            .then((r) => {
                setLoading(true);
                hideDialog();
                toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Ingresado Correctamente', life: 1500 });
            })
            .catch((e) => {
                toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
            })
    }



    /* MODAL ELIMINAR */
    //abrir modal eliminar
    const OpenDeleteModal = (id) => {
        setCategoriaId(id);
        setDeleteModal(true);
    }

    //cerrar modal eliminar
    const hideDeleteModal = () => {
        setCategoriaId("");
        setDeleteModal(false);
    };

    //mandar datos del eliminar a la api
    const EliminarCategorias = (e) => {

        let payload = {
            cate_Id: CategoriaId,
        }
        axios.post(Global.url + 'Categoria/Eliminar', payload)
            .then((r) => {
                hideDeleteModal();
                setLoading(true);
                setCategoriaId("");
                toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Eliminado Correctamente', life: 1500 });
            })
            .catch((e) => {
                toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
            })
    }

    /* MODAL EDITAR */
    //traer los datos para el editar
    const LlamarDatosEdit = (id) => {
        axios.get(Global.url + 'Categoria/Buscar?id=' + id)
            .then((r) => {
                return r.data; // La respuesta ya está en formato JSON
            })
            .then((data) => {
                setDescripcion(data.cate_Descripcion)
                setEditModal(true)
                setCategoriaId(id);
            })
            .catch(error => console.error(error))
    }

    //cerrar modal Editar
    const hideEditModal = () => {
        setDescripcion("");
        setError('');
        setCategoriaId('');
        setEditModal(false);
    };

    //validar campos vacios modal Editar
    const ValidarDatosEdit = () => {
        if (!Desripcion) {
            setError('Este campo no necesario.');
            return;
        }
        else {
            DatosEditados();
        }
    };

    //Mandar la categoria ya editada a la api
    const DatosEditados = (e) => {
        let payload = {
            cate_Id: CategoriaId,
            cate_Descripcion: Desripcion,
            cate_usuModificacion: 1
        }
        axios.post(Global.url + 'Categoria/Editar', payload)
            .then((r) => {
                hideEditModal();
                setLoading(true);
                toast.current.show({ severity: 'success', summary: 'Accion Exitosa', detail: 'Registro Editado Correctamente', life: 1500 });
            })
            .catch((e) => {
                toast.current.show({ severity: 'warn', summary: 'Advertencia', detail: 'Ups, algo salió mal. ¡Inténtalo nuevamente!', life: 2000 });
            })
    }

    return (
        <div className="grid">
            <div className="col-12">

                <div className="card" style={{ background: `rgb(105,101,235)`, height: '100px', width: '100%' }}>
                    <div className="row text-center d-flex align-items-center">
                        <h2 style={{ color: 'white' }}>Categorias</h2>
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
                        dataKey="cate_Id"
                        filterDisplay="menu"
                        responsiveLayout="scroll"
                        emptyMessage="No se encontrron registros."
                        filterMode="filter"
                        header={header}
                        value={posts.filter((post) =>
                            post.cate_Descripcion.toLowerCase().includes(searchText.toLowerCase())
                        )}

                    >
                        <Column field="cate_Descripcion" header="Nombre" headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }} />
                        <Column
                            field="acciones"
                            header="Acciones"
                            headerStyle={{ background: `rgb(105,101,235)`, color: '#fff' }}
                            style={{ minWidth: '300px' }}
                            body={rowData => (
                                <div>
                                    <Button label="Detalles" severity="info" icon="pi pi-eye" outlined style={{ fontSize: '0.8rem' }} /> .
                                    <Button label="Editar" severity="warning" icon="pi pi-upload" outlined style={{ fontSize: '0.8rem' }} onClick={() => LlamarDatosEdit(rowData.cate_Id)} /> .
                                    <Button label="Eliminar" severity="danger" icon="pi pi-trash" outlined style={{ fontSize: '0.8rem' }} onClick={() => OpenDeleteModal(rowData.cate_Id)} />
                                </div>
                            )}
                        />
                    </DataTable>

                    {/*modal para Ingresar datos*/}
                    <Dialog
                        visible={productDialog}
                        style={{ width: '450px' }}
                        header="Ingresar Categoria"
                        modal
                        className="p-fluid"
                        onHide={hideDialog}
                        footer={
                            <div>
                                <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDialog} />
                                <Button label="Guardar" icon="pi pi-check" severity="success" onClick={saveProduct} />
                            </div>
                        }
                    >
                        <div className="field">
                            <label htmlFor="name">Categoria</label>
                            <InputText
                                id="name"
                                value={Desripcion}
                                onChange={(e) => setDescripcion(e.target.value)}
                                required
                            />
                            {error && <div className="error" style={{ color: 'red' }}>{error}</div>}
                        </div>
                    </Dialog>

                    {/*modal para editar datos*/}
                    <Dialog
                        visible={EditModal}
                        style={{ width: '450px' }}
                        header="Editar Categoria"
                        modal
                        className="p-fluid"
                        onHide={hideEditModal}
                        footer={
                            <div>
                                <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideEditModal} />
                                <Button label="Guardar" icon="pi pi-check" severity="success" onClick={ValidarDatosEdit} />
                            </div>
                        }
                    >
                        <div className="field">
                            <label htmlFor="name">Categoria</label>
                            <InputText
                                id="name"
                                value={Desripcion}
                                onChange={(e) => setDescripcion(e.target.value)}
                                required
                            />
                            {error && <div className="error" style={{ color: 'red' }}>{error}</div>}
                        </div>
                    </Dialog>



                    {/*modal para Eliminar Registros*/}
                    <Dialog visible={DeleteModal} style={{ width: '450px' }} header="Eliminar Categorias" onHide={hideDeleteModal} modal footer={
                        <>
                            <Button label="Cancelar" icon="pi pi-times" severity="danger" onClick={hideDeleteModal} />
                            <Button label="Confirmar" icon="pi pi-check" severity="success" onClick={EliminarCategorias} />
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



export default App;

