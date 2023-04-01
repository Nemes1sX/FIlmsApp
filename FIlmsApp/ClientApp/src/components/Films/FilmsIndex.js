import React, { useEffect, useState } from 'react';
import {Link } from 'react-router-dom';

export default function FilmsIndex() {
    const [films, setFilms] = useState([]);
    const [filmsWithOutFilter, setFIlmsWIthoutFilter] = useState([]);
    const [filmFilter, setFilmsFilter] = useState({
        name: "",
        genre: "",
        actor: ""
    });

    useEffect(() => {
        getFilms();
    }, [])

    function getFilms() {
        fetch("api/films/index")
            .then(response => response.json())
            .then(data => {
                setFilms(data);
                console.log(films);
            });
            console.log(films);

    }
    

    function filterFilms() {
        var filmIdFilter = this.state.FilmId;
        var filmNameFilter = this.state.FilmNameFilter;
    }
    
    function deleteFilm(id) {
        alert('Delete film' + id);
    }

    function editFilm(id) {
        alert('Edit film' + id);
    }

    function sortResult(filmFilter) {
        alert('FIlm filter' + filmFilter);
    }

    return (
            <div>
                <Link to='/film/add' className="btn btn-success">
                    Add Film
                </Link>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th width="10px">
                                <div className="d-flex flex-row">

                                    <input className="form-control m-2"
                                        placeholder="Filter" />

                                    <button type="button" className="btn btn-light"
                                        onClick={() => sortResult('FilmName', true)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                            <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>


                                </div>
                              Id
                            </th>
                            <th>
                                Film Name
                            </th>
                            <th>
                                Actors
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {films.map(fil =>
                            <tr key={fil.id}>
                                <td>{fil.id}</td>
                                <td>{fil.name}</td>
                                <td>{fil.actors.map(actor => <p key={actor.id}>{actor.name}</p>)}</td>

                                <td>
                                    <button type="button"
                                        className="btn btn-light mr-1"
                                        data-bs-toggle="modal"
                                        data-bs-target="#exampleModal"
                                        onClick={() => editFilm(fil.Id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>

                                    <button type="button"
                                        className="btn btn-light mr-1"
                                        onClick={() => deleteFilm(fil.Id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>

                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
 
            </div >
        )
    }