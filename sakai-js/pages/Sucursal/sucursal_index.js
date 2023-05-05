import React, { Component } from 'react';
import { Button } from 'primereact/button';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import Global from '../api/Global';
import { InputText } from 'primereact/inputtext';


class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            posts: [],
            searchText: ''
        };

        this.setSearchText = this.setSearchText.bind(this);
    }

    setSearchText(event) {
        this.setState({ searchText: event.target.value });
    }


    componentDidMount() {
        const url = Global.url + 'Sucursal/Listado';
        fetch(url)
            .then(response => response.json())
            .then(data => this.setState({ posts: data.data }))
    }


    render() {

        const { posts, searchText } = this.state;

        const header = (
            <div className="table-header flex flex-column md:flex-row md:justify-content-between md:align-items-center">
                <Button label="Nuevo" severity="success" outlined />
              <span className="block mt-2 md:mt-0 p-input-icon-left">
                <i className="pi pi-search" />
                <InputText type="text" value={searchText} onChange={this.setSearchText} placeholder="Buscar..." />
              </span>
            </div>
        );
          
          

        return (
            <div className="grid">
                <div className="col-12">

                    <div className="card" style={{background: `linear-gradient(rgb(105,101,235), rgb(155, 178, 250))`, height: '100px', width: '100%' }}>
                        <div className="row text-center d-flex align-items-center">
                            <h2 style={{ color: 'white' }}>Sucursal</h2>
                        </div>
                    </div>


                    <div className="card">

                        <DataTable
                            paginator
                            className="p-datatable-gridlines"
                            showGridlines
                            sortField="representative.name"
                            rows={10}
                            rowsPerPageOptions={[5, 10, 25]}
                            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                            currentPageReportTemplate="Mostrando {first} a {last} de {totalRecords} registros."
                            dataKey="sucu_Id"
                            filterDisplay="menu"
                            responsiveLayout="scroll"
                            emptyMessage="No se encontrron registros."
                            filterMode="filter"
                            header={header}
                            value={posts.filter((post) =>
                                post.sucu_Nombre.toLowerCase().includes(searchText.toLowerCase()) ||
                                post.muni_Nombre.toLowerCase().includes(searchText.toLowerCase()) ||
                                post.depa_Nombre.toLowerCase().includes(searchText.toLowerCase())
                            )}

                        >
                            <Column field="sucu_Nombre" header="Nombre" headerStyle={{ background: `linear-gradient(rgb(105,101,235), rgb(155, 178, 250))`, color: '#fff' }} />
                            <Column field="muni_Nombre" header="Municipio" headerStyle={{ background: `linear-gradient(rgb(105,101,235), rgb(155, 178, 250))` , color: '#fff'}} />
                            <Column field="depa_Nombre" header="Departamento" headerStyle={{ background: `linear-gradient(rgb(105,101,235), rgb(155, 178, 250))`, color: '#fff' }} />
                            <Column
                                field="acciones"
                                header="Acciones"
                                headerStyle={{ background: `linear-gradient(rgb(105,101,235), rgb(155, 178, 250))`, color: '#fff'}}
                                style={{ minWidth: '300px' }}
                                body={rowData => (
                                    <div>
                                        <Button label="Detalles" severity="info" outlined style={{ fontSize: '0.8rem' }} /> .
                                        <Button label="Editar" severity="warning" outlined style={{ fontSize: '0.8rem' }} /> .
                                        <Button label="Eliminar" severity="danger" outlined style={{ fontSize: '0.8rem' }} />
                                    </div>
                                )}
                            />
                        </DataTable>
                    </div>
                </div>
            </div>
        )
    }
}


export default App;

