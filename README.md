# Containerize .NET and Deploy to Kubernetes

## Tested with

- [.NET SDK 5.0.103](https://dotnet.microsoft.com/download/visual-studio-sdks)
- [Docker Desktop 3.5.2](https://www.docker.com/products/docker-desktop)
- [Pack CLI 0.20.0](https://buildpacks.io/docs/tools/pack/)

## Demo Script

- Build app, run it
  - `dotnet new mvc -o mvc-demo`
  - `dotnet run`
- Containerize the app, run in Docker
  - [Dockerfile](https://docs.docker.com/engine/reference/builder/)
    - `docker image build -t mvc-demo-dockerfile .`
    - `docker container run -p 80:80 mvc-demo-dockerfile`
  - [Buildpacks](https://buildpacks.io/)
    - `rm Dockerfile`
    - `pack build mvc-demo-buildpacks`
    - `docker container run -p 80:80 mvc-demo-buildpacks`
  - Push app to an image registry
    - `docker image tag mvc-demo-buildpacks fjb4/mvc-demo-buildpacks`
    - `docker image push fjb4/mvc-demo-buildpacks`
    - [View image on Docker Hub](https://hub.docker.com/repository/docker/fjb4/mvc-demo-buildpacks)
- Run in [Kubernetes](https://kubernetes.io/)
  - Local Kubernetes
    - [Enable Kubernetes in Docker Desktop](https://docs.docker.com/desktop/kubernetes/)
    - Intro to [kubectl](https://kubernetes.io/docs/tasks/tools/)
      - `kubectl config get-contexts`
      - `kubectl get pod`
    - Deploy to Kubernetes
      - `kubectl create deployment mvc-demo --image=fjb4/mvc-demo-buildpacks --port=8080 --replicas=1 --dry-run -o yaml > deploy.yaml`
      - `kubectl expose deployment/mvc-demo --type=LoadBalancer --port=80 --target-port=8080 --dry-run -o yaml > service.yaml`
      - `kubectl scale deployment/mvc-demo --replicas=5`
      - `kubectl logs <pod-name>`
  - Cloud Kubernetes
    - Options
      - [Google Kubernetes Engine (GKE)](https://cloud.google.com/kubernetes-engine)
      - [Azure Kubernetes Service (AKS)](https://azure.microsoft.com/en-us/services/kubernetes-service)
      - [Amazon Elastic Kubernetes Service (EKS)](https://aws.amazon.com/eks)
    - Deploy to GKE
      - [Create GKE cluster](https://console.cloud.google.com)
      - Connect to GKE cluster
      - `kubectl apply -f deploy.yaml`
      - `kubectl apply -f service.yaml`
      - `kubectl scale deployment/mvc-demo --replicas=5`
        - Show pod name and IP change when page refreshed
      - `kubectl logs <pod-name>`
    - Deploy SQL Server to Kubernetes
      - `kubectl apply -f sql-deploy.yaml`
  