FROM centos:7
FROM python:3.5-alpine
COPY . /home
WORKDIR /home
RUN pip install -r requirements.txt
CMD python3 run.py