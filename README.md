# LibraryServiceNAGP Multi-Tier Application on Kubernetes

This repository contains the source code, Dockerfile, and Kubernetes YAML configurations for a multi-tier application. The application consists of a .NET Core Web API (Service API Tier) that fetches data from a PostgreSQL database (Database Tier), all deployed on a Google Kubernetes Engine cluster.

## Code Repository

You are currently viewing the code repository.
**Repository URL:** [https://github.com/Trilokpgit/LibraryServiceNAGP](https://github.com/Trilokpgit/LibraryServiceNAGP)

## Docker Hub Image

Below is The Docker image for this Library Service API is hosted on Docker Hub.

**Docker Hub URL:** [https://hub.docker.com/r/trilokp/libraryservice](https://hub.docker.com/r/trilokp/libraryservice)

## Service API Endpoint

Once deployed, the Service API can be accessed externally via the following URL. This endpoint will fetch and display records of Books from the PostgreSQL database.

**Service API URL:** [http://34.71.8.0/api/library](http://34.71.8.0/api/library)

## Setup and Deployment Instructions

1.  **Prerequisites:**
    * .NET Core SDK
    * Docker
    * `kubectl`
    * Google Cloud SDK (`gcloud`) or Cloud Shell
    * A Google Cloud Project with Billing Enabled

2.  **Build and Push Docker Image:**
    ```bash
    cd src/LibraryService/LibraryService
    docker build -t dockerhub-username/service-api:latest .
    docker push dockerhub-username/service-api:latest
    ```
  
3.  **Google Kubernetes Engine Cluster Setup:**
    ```bash
    gcloud config set project your-gcp-project-id
    gcloud container clusters create your-cluster-name --num-nodes=2 --zone your-zone
    gcloud container clusters get-credentials your-cluster-name --zone your-zone
    ```

4.  **Create Kubernetes Secrets and ConfigMaps:**
    * Edit `Kubernetes/secret.yml` with your base64 encoded database password.
        ```bash
        echo -n "your_strong_db_password" | base64
        ```
    * Apply:
        ```bash
        kubectl apply -f Kubernetes/configmap.yml
        kubectl apply -f Kubernetes/secret.yml
        ```

5.  **Deploy PostgreSQL Database:**
    ```bash
    kubectl apply -f Kubernetes/postgress-service.yml
    kubectl apply -f Kubernetes/postgres-statefulset.yml
    ```
    *Wait until the PostgreSQL pod is `Running` before deploying the API service.*

6.  **Deploy .NET Core Service API:**
    ```bash
    kubectl apply -f Kubernetes/api-service.yml
    kubectl apply -f Kubernetes/api-deployment.yml
    ```

7.  **Expose Service API with Ingress:**
    ```bash
    kubectl apply -f Kubernetes/ingress.yml
    ```
    *It may take a few minutes for the Ingress IP address to be provisioned.*
    `kubectl get ingress`

## Kubernetes Resources Used

* `ConfigMap`: `app-config` - Stores non-sensitive database connection details.
* `ConfigMap`: `init-sql` - Stores SQL script to initially seed the data.
* `Secret`: `db-secret` - Securely stores the database password.
* `Service (ClusterIP)`: `postgres` - Internal service discovery for PostgreSQL.
* `StatefulSet`: `postgres` - Manages the single, stateful PostgreSQL database pod with persistence.
* `Service (ClusterIP)`: `dotnet-api-service` - Internal service discovery for the .NET Core API.
* `Deployment`: `dotnet-api` - Manages 4 replicas of the stateless .NET Core API pods with rolling updates.
* `Ingress`: `api-ingress` - Exposes the Service API externally via HTTP/HTTPS.

## API Endpoints

* **GET `/api/library`**: Fetches all books from the PostgreSQL database.
    * Example Response:
        ```json
        {
          "data": [
              {
                  "id": 1,
                  "name": "Atomic Habits",
                  "description": "Tiny Changes, Remarkable Results."
              }
          ],
          "success": true,
          "statusCode": 200,
          "error": null
        }
        ```

## NOTE:
The Dockerfile in the root directory is for reference only. The actual Dockerfile used to build the image is located inside the LibraryService directory.
---




